using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public sealed class GroupResponse:BaseResponse
    {
        public string title { get; set; }
        public string description { get; set; }
        public Guid? operatinghourid { get; set; }
        public string agents { get; set; }
        public string leader { get; set; }
        public string supportemail { get; set; }
    }
}
