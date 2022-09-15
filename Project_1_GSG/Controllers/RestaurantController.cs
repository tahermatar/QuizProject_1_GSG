using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_1_GSG.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Project_1_GSG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private restaurantdbContext _restaurantdbContext;
        public RestaurantController(restaurantdbContext restaurantdbContext)
        {
            _restaurantdbContext = restaurantdbContext;
        }
        [HttpPost]
        public string Add(Restaurant Restaurant)
        {
            try
            {
                _restaurantdbContext.Restaurants.Add(Restaurant);
                _restaurantdbContext.SaveChanges();

                return "Restaurant details saved successfuly.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        //public IEnumerable<Restaurant> Get()
        //{
        //    return _restaurantdbContext.Restaurants.ToList();
        //}
        [HttpGet]
        public IActionResult Get()
        {
            var viewRes = _restaurantdbContext.CsvViews.ToList();

            //var modelView = _mapper.Map<List<CsvModelView>>(viewRes);

            var res = _restaurantdbContext.Restaurants.ToList();

            using (var writer = new StreamWriter("E:\\ASP.net GSG\\Part_2 Projects\\QuizProject_1_GSG\\ItemsDB.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(viewRes);
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            var Restaurant = _restaurantdbContext.Restaurants.Find(Id);

            return Ok(Restaurant);
        }
        [HttpPut("{id}")]
        public string Update(int Id, Restaurant restaurant)
        {
            var existingRestaurant = _restaurantdbContext.Restaurants.Find(Id);
            if (existingRestaurant != null)
            {
                existingRestaurant.Name = restaurant.Name;
                existingRestaurant.PhoneNumber = restaurant.PhoneNumber;

                _restaurantdbContext.Entry(existingRestaurant).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                _restaurantdbContext.SaveChanges();

                return "Restaurant details saved successfuly.";
            }
            else
            {
                return $"Restaurant details not avialable with {Id}.";
            }
        }
        [HttpDelete("{id}")]
        public string Delete(int Id)
        {
            var restaurant = _restaurantdbContext.Restaurants.Find(Id);

            if (restaurant != null)
            {
                _restaurantdbContext.Restaurants.Remove(restaurant);
                _restaurantdbContext.SaveChanges();

                return "Restaurant details deleted successfuly.";
            }
            else
            {
                return $"Restaurant details not avialable with {Id}.";
            }
        }
    }
}
