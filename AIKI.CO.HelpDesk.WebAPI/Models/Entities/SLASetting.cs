using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class SLASetting : BaseObject
    {
        public string title { get; set; }
        public string description { get; set; }
        public Guid? operatinghourid { get; set; }
        public TargetsPriority[] targetspriority { get; set; }
        public RequestTypePriority[] requesttypepriority { get; set; }
        public Company Company { get; set; }
    }
}