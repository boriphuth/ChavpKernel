// StatisticsRequest.cs

using System.Collections.Generic;

namespace Chavp.Kernel.Commands
{
    public struct StatisticsRequest
    {
        public string Command { get; set; }
        public IList<double> Values { get; set; }
    }
}
