using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_1_GSG.Models;
using Project_1_GSG.ModelView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Project_1_GSG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantMenuController : ControllerBase
    {
            private restaurantdbContext _restaurantdbContext;
            private IMapper _mapper;
            public RestaurantMenuController(restaurantdbContext restaurantdbContext)
            {
                _restaurantdbContext = restaurantdbContext;
            }
            [HttpPost]
            public string Add(Restaurantmenu Restaurantmenu)
            {
                try
                {
                    _restaurantdbContext.Restaurantmenus.Add(Restaurantmenu);
                    _restaurantdbContext.SaveChanges();

                    return "RestaurantMenu details saved successfuly.";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            //[HttpGet]
            
            //public IEnumerable<Restaurantmenu> Get()
            //{
            //    return _restaurantdbContext.Restaurantmenus.ToList();
            //}

            [HttpGet]
            public IActionResult Get()
            {
            var viewRes = _restaurantdbContext.CsvViews.ToList();

            var modelView = _mapper.Map<List<CsvModelView>>(viewRes);

            var res = _restaurantdbContext.Restaurantmenus.ToList();

            using (var writer = new StreamWriter("E:\\ASP.net GSG\\Part_2 Projects\\QuizProject_1_GSG\\ItemsDB.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(modelView);
            }
            return Ok();
            }
            
            [HttpGet("{id}")]
            public IActionResult Get(int Id)
            {
                var RestaurantMenu = _restaurantdbContext.Restaurantmenus.Find(Id);
            
                return Ok(RestaurantMenu);
            }
            [HttpPut("{id}")]
            public string Update(int Id, Restaurantmenu restaurantmenu)
            {
                var existingRestaurantmenu = _restaurantdbContext.Restaurantmenus.Find(Id);
                if (existingRestaurantmenu != null)
                {
                existingRestaurantmenu.MealName = restaurantmenu.MealName;
                existingRestaurantmenu.PriceInNis = restaurantmenu.PriceInNis;
                existingRestaurantmenu.PriceInUsd = restaurantmenu.PriceInUsd;
                existingRestaurantmenu.Quantity = restaurantmenu.Quantity;

                _restaurantdbContext.Entry(existingRestaurantmenu).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                _restaurantdbContext.SaveChanges();
            
                    return "Restaurantmenu details saved successfuly.";
                }
                else
                {
                    return $"Restaurantmenu details not avialable with {Id}.";
                }
            }
            [HttpDelete("{id}")]
            public string Delete(int Id)
            {
                var restaurantMenu = _restaurantdbContext.Restaurantmenus.Find(Id);
            
                if (restaurantMenu != null)
                {
                    _restaurantdbContext.Restaurantmenus.Remove(restaurantMenu);
                    _restaurantdbContext.SaveChanges();
            
                    return "Restaurantmenu details deleted successfuly.";
                }
                else
                {
                    return $"Restaurantmenu details not avialable with {Id}.";
                }
            }
    }       
}
