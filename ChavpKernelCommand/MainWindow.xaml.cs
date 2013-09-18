using Chavp.Kernel.Commands;
using ChavpKernelCommand.Properties;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChavpKernelCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBus _bus;
        IPublishChannel _publishChannel;

        CommandViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();

            string rabbitMQBrokerHost = Settings.Default.Host;
            string virtualHost = Settings.Default.VirtualHost;
            string username = Settings.Default.UserName;
            string password = Settings.Default.Password;

            string connectionString = string.Format(
                "host={0};virtualHost={1};username={2};password={3}",
                rabbitMQBrokerHost, virtualHost, username, password);

            _bus = RabbitHutch.CreateBus(connectionString);

            _publishChannel = _bus.OpenPublishChannel();

            _vm = new CommandViewModel(_publishChannel);
            _vm.Command = "{ command: \"sum\", values: [1,2,3,4,5,6,7,8,9] }";

            DataContext = _vm;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_bus != null)
                _bus.Dispose();

            base.OnClosed(e);
        }
    }
}
