using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class AssetsViewConfiguration : BaseViewConfiguration<AssetsView>
    {
        public AssetsViewConfiguration(Guid companyid) : base(companyid)
        {
        }
    }
}