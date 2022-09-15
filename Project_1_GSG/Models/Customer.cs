using System;
using System.Collections.Generic;

#nullable disable

namespace Project_1_GSG.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
