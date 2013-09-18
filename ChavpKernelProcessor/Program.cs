using Chavp.Kernel.Commands;
using ChavpKernelProcessor.Properties;
using EasyNetQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChavpKernelProcessor
{
    class Program
    {

        static void Main(string[] args)
        {
            string processorName = Settings.Default.Name;

            string rabbitMQBrokerHost = Settings.Default.Host;
            string virtualHost = Settings.Default.VirtualHost;
            string username = Settings.Default.UserName;
            string password = Settings.Default.Password;

            string connectionString = string.Format(
                "host={0};virtualHost={1};username={2};password={3}",
                rabbitMQBrokerHost, virtualHost, username, password);

            using (IBus bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.RespondAsync<Request, Response>(request =>
                Task.Factory.StartNew(() =>
                {
                    string reqMessage = string.Format(
                        "Request: {0}, From: {1}, Command: {2}",
                        request.Date, request.Name, request.Command);

                    Console.WriteLine(reqMessage);
                    
                    var resp = new Response
                    {
                        Name = processorName,
                        Date = DateTime.Now,
                    };


                    try
                    {
                        var comm = JsonConvert.DeserializeObject<StatisticRequest>(request.Command.ToLower());

                        if (comm.Command == "sum")
                        {
                            resp.Return = JsonConvert.SerializeObject(new 
                            {
                                sum = comm.Values.Sum(),
                            });
                        }
                        else if (comm.Command == "average")
                        {
                            resp.Return = JsonConvert.SerializeObject(new
                            {
                                average = comm.Values.Average(),
                            });
                        }
                        else
                        {
                            resp.Return = "unrecognized command " + comm.Command;
                        }

                        string respMessage = string.Format(
                            "Response: {0}, Return: {1}",
                            resp.Date, resp.Name);
                    }
                    catch (Exception ex)
                    {
                        resp.Return = "exception from server: " + ex.Message;
                    }

                    return resp;
                }));

                Console.ReadLine();
            }
        }
    }
}
