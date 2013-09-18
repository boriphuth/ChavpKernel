using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chavp.Kernel.Commands
{
    public struct Request
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Command { get; set; }
    }
}
