using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chavp.Kernel.Commands
{
    public struct StatisticsRequest
    {
        public string Command { get; set; }
        public List<double> Values { get; set; }
    }
}
