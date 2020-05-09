using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class AssetResponse:BaseResponse
    {
        public Guid employeeid { get; set; }
        public Guid assetlocationid { get; set; }
        public Guid assettypeid { get; set; }
        public string assetnumber { get; set; }
        public AssetAdditionalInfo[] assetadditionalinfo { get; set; }
    }
}