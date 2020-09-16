using System;
using System.ComponentModel.DataAnnotations;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class SLASettingResponse : BaseResponse
    {
        [Required] public string title { get; set; }

        public string description { get; set; }
        public Guid? operatinghourid { get; set; }
        public TargetsPriority[] targetspriority { get; set; }
        public RequestTypePriority[] requesttypepriority { get; set; }
    }
}