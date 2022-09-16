using System;
using System.Collections.Generic;

#nullable disable

namespace Project_1_GSG.Models
{
    public partial class Restaurantmenu
    {
        public Restaurantmenu()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string MealName { get; set; }
        public float PriceInNis { get; set; }
        public float PriceInUsd => (float)(PriceInNis / 3.5);
        public int Quantity { get; set; }
        public int RestaurantId { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public bool Archived { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
