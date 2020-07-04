using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class SLASettingResponse : BaseResponse
    {
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        public Guid? operatinghourid { get; set; }
        public TargetsPriority[] targetspriority { get; set; }
        public RequestTypePriority[] requesttypepriority { get; set; }
    }
}