using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class OrganizeChart : BaseObject
    {
        public OrganizeChart()
        {
            OrganizeCharts = new HashSet<OrganizeChart>();
        }

        public Guid? parent_id { get; set; }
        public string title { get; set; }
        public OrganizeChartAdditionalInfo[] additionalinfo { get; set; }

        public Company Company { get; set; }
        public OrganizeChart ParentOrganizeChart { get; set; }
        public ICollection<OrganizeChart> OrganizeCharts { get; set; }
    }
}