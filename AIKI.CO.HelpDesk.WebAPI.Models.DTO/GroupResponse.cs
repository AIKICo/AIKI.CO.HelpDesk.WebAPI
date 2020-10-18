using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public sealed class GroupResponse : BaseResponse
    {
        [Required] public string title { get; set; }

        public string description { get; set; }
        public Guid? operatinghourid { get; set; }
        public string agents { get; set; }
        public string leader { get; set; }
        public string supportemail { get; set; }
    }
}