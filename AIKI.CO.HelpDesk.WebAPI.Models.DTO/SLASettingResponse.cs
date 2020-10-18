using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
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