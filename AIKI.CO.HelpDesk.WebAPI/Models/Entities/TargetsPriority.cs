using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class TargetsPriority
    {
        public string Title { get; set; }
        public string ResponseTime { get; set; }
        public string ResponseTimeUnit { get; set; }
        public string ResolveTime { get; set; }
        public string ResolveTimeUnit { get; set; }
    }
}