using System;
using System.Collections.Generic;

#nullable disable

namespace Project_1_GSG.Models
{
    public partial class Order
    {
        public int ResturantMenuId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public bool Archived { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Restaurantmenu ResturantMenu { get; set; }
    }
}
