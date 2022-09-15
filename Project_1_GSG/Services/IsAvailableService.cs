using Project_1_GSG.Models;
using System;

namespace Project_1_GSG.Services
{
    public class IsAvailableService
    {
        private restaurantdbContext _restaurantdbContext;

        public IsAvailableService()
        {
        }

        public IsAvailableService(restaurantdbContext restaurantdbContext)
        {
            _restaurantdbContext = restaurantdbContext;
        }
        
        public bool isAvailable(int Id)
        {
            var res = _restaurantdbContext.Restaurantmenus.Find(Id);

            if (res.Quantity > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
