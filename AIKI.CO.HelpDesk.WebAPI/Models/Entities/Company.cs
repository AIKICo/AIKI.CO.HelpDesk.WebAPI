using System;
using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class Company
    {
        public Company()
        {
            Customers = new HashSet<Customer>();
            Members = new HashSet<Member>();
            OperatingsHour = new HashSet<OperatingHour>();
            SLASettings = new HashSet<SLASetting>();
            Groups = new HashSet<Group>();
        }
        public Guid id { get; set; }
        public string title { get; set; }
        public string email { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Member> Members { get; set; }
        public ICollection<OperatingHour> OperatingsHour { get; set; }
        public ICollection<SLASetting> SLASettings { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
