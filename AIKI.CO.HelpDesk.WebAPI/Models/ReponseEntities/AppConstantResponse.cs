using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class AppConstantResponse : BaseResponse
    {
        [Required]
        public string title { get; set; }
        public string name { get; set; }
    }
}