using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class OrganizeChartViewConfiguration: BaseViewConfiguration<OrganizeChartView>
    {
        public OrganizeChartViewConfiguration(Guid companyid) : base(companyid)
        {
        }
    }
}