using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_1_GSG.Models;
using Project_1_GSG.Services;

namespace Project_1_GSG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private restaurantdbContext _restaurantdbContext;
        public OrderController(restaurantdbContext restaurantdbContext)
        {
            _restaurantdbContext = restaurantdbContext;
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            var IsAvailable = new IsAvailableService();
            //IsAvailable.isAvailable(1);

            _restaurantdbContext.Orders.Add(order);
            _restaurantdbContext.SaveChanges();

            return Ok("Order details saved successfuly.");

        }
        [HttpPut("{id}")]
        public string Update(int Id, Order order)
        {
            var IsAvailable = new IsAvailableService();
            //IsAvailable.isAvailable(1);
            var existingOrder = _restaurantdbContext.Orders.Find(Id);
            if (existingOrder != null)
            {
                existingOrder.ResturantMenuId = order.ResturantMenuId;
                existingOrder.CustomerId = order.CustomerId;

                _restaurantdbContext.Entry(existingOrder).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                _restaurantdbContext.SaveChanges();

                return "Order details saved successfuly.";
            }
            else
            {
                return $"Order details not avialable with {Id}.";
            }
        }
        [HttpDelete("{id}")]
        public string Delete(int Id)
        {
            var order = _restaurantdbContext.Orders.Find(Id);

            if (order != null)
            {
                _restaurantdbContext.Orders.Remove(order);
                _restaurantdbContext.SaveChanges();

                return "Order details deleted successfuly.";
            }
            else
            {
                return $"Order details not avialable with {Id}.";
            }
        }
    }
}

