using System;
using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class Company
    {
        public Company()
        {
            Customer = new HashSet<Customer>();
            Member = new HashSet<Member>();
            OperatingHour = new HashSet<OperatingHour>();
        }
        public Guid id { get; set; }
        public string title { get; set; }
        public string email { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<Member> Member { get; set; }
        public ICollection<OperatingHour> OperatingHour { get; set; }
    }
}
