using Chavp.Kernel.Commands;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ChavpKernelCommand
{
    public class CommandViewModel: INotifyPropertyChanged
    {
        private string _Command;
        private string _Result;

        public CommandViewModel(IPublishChannel publishChannel)
        {
            RequestCommand = new RequestCommand(this);
            PublishChannel = publishChannel;
        }

        public ICommand RequestCommand { get; set; }
        public IPublishChannel PublishChannel { get; private set; }
        public string Command
        {
            get { return _Command; }
            set
            {
                _Command = value;
                RaisePropertyChanged("Command");
            }
        }
        public string Result
        {
            get { return _Result; }
            set
            {
                _Result = value;
                RaisePropertyChanged("Result");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
