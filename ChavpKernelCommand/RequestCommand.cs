using Chavp.Kernel.Commands;
using ChavpKernelCommand.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ChavpKernelCommand
{
    public class RequestCommand : ICommand
    {
        CommandViewModel _vm;
        public RequestCommand(CommandViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var request = new Request
            {
                Name = Settings.Default.Name,
                Date = DateTime.Now,
                Command = _vm.Command
            };

            string result = string.Format("Request: {0}, Command: {1}\n"
                , request.Date.ToString("dd/MM/yyyy HH:mm:ss"), request.Command);

            _vm.PublishChannel.Request<Request, Response>(
                request, response =>
                {
                    result += string.Format(
                        "Response: {0}, From: {1}, Result: {2}\n"
                        , response.Date.ToString("dd/MM/yyyy HH:mm:ss"), response.Name, response.Return);

                    _vm.Result += result;
                    _vm.Result += string.Format("Elapsed time: {0}\n", response.Date - request.Date);
                    _vm.Result += "-------------------------------------------------------------------\n";
                });
        }
    }
}
