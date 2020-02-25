using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class BaseObject
    {
        public Guid id { get; set; }
        public Guid? companyid { get; set; }
    }
}
