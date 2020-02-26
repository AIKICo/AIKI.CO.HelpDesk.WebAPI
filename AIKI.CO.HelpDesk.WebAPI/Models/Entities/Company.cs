using System;
using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public partial class Company
    {
        public Company()
        {
            Customer = new HashSet<Customer>();
            Member = new HashSet<Member>();
        }
        public Guid id { get; set; }
        public string title { get; set; }
        public string email { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Member> Member { get; set; }
    }
}
