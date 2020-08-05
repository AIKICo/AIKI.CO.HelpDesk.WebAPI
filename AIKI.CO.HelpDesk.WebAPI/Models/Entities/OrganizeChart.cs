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
        public string titletype { get; set; }
        public Guid customerid { get; set; }
        public OrganizeChartAdditionalInfo[] additionalinfo { get; set; }
        public string email { get; set; }

        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public OrganizeChart ParentOrganizeChart { get; set; }
        public ICollection<OrganizeChart> OrganizeCharts { get; set; }
    }
}