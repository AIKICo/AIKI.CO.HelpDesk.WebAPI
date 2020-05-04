using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class Holiday
    {
        public DateTime Day { get; set; }
        public string Reason { get; set; }
    }
}