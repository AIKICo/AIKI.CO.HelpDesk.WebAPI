﻿using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class AppConstantResponse : BaseResponse
    {
        [Required] public string title { get; set; }

        public string name { get; set; }
    }
}