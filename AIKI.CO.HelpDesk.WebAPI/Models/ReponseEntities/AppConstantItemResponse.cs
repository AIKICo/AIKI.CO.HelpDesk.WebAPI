using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class AppConstantItemResponse:BaseResponse
    {
        public Guid appconstantid { get; set; }
        public string value1 { get; set; }
        public string value2 { get; set; }
    }
}
