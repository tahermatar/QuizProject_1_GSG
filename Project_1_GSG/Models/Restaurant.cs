using System;
using System.Collections.Generic;

#nullable disable

namespace Project_1_GSG.Models
{
    public partial class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public bool Archived { get; set; }
        public virtual Restaurantmenu Restaurantmenu { get; set; }
    }
}
