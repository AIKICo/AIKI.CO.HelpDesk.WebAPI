using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public sealed class CustomerResponse : BaseResponse
    {
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        public string domains { get; set; }
        public byte?[] schema { get; set; }
        public bool? disabled { get; set; }
        public Guid? operatinghourid { get; set; }
    }
}