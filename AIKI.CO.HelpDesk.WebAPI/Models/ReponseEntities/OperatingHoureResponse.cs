using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class OperatingHoureResponse : BaseResponse
    {
        [Required]
        public string title { get; set; }
        public string timezone { get; set; }
        public Workday[] workdays { get; set; }
        public Holiday[] holidays { get; set; }
        public bool? isdefault { get; set; }
    }
}