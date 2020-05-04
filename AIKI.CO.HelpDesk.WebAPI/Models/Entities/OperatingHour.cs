using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class OperatingHour : BaseObject
    {
        public string title { get; set; }
        public string timezone { get; set; }
        public Workday[] workdays { get; set; }
        public Holiday[] holidays { get; set; }
        public bool? isdefault { get; set; }

        public Company Company { get; set; }
    }
}