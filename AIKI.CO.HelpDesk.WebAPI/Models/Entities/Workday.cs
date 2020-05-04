using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class Workday
    {
        public string DayName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}