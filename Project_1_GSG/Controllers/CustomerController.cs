using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_1_GSG.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_1_GSG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private restaurantdbContext _restaurantdbContext;
        public CustomerController(restaurantdbContext restaurantdbContext)
        {
            _restaurantdbContext = restaurantdbContext;
        }
        [HttpPost]
        public string Add(Customer Customer)
        {
            try
            {
                _restaurantdbContext.Customers.Add(Customer);
                _restaurantdbContext.SaveChanges();

                return "Customer details saved successfuly.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _restaurantdbContext.Customers.ToList();
        }

        //Or

        //public IActionResult Get()
        //{
        //    var res = _restaurantdbContext.Customers.ToList();
        //    return Ok(res);
        //}
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            var customer = _restaurantdbContext.Customers.Find(Id);
            return Ok(customer);
        }
        [HttpPut("{id}")]
        public string Update(int Id, Customer customer)
        {
            var existingCustomer = _restaurantdbContext.Customers.Find(Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;

                _restaurantdbContext.Entry(existingCustomer).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
                _restaurantdbContext.SaveChanges();

                return "Customer details saved successfuly.";
            }
            else
            {
                return $"Customer details not avialable with {Id}.";
            }
        }
        [HttpDelete("{id}")]
        public string Delete(int Id)
        {
            var customer = _restaurantdbContext.Customers.Find(Id);

            if(customer != null)
            {
                _restaurantdbContext.Customers.Remove(customer);
                _restaurantdbContext.SaveChanges();

                return "Customer details deleted successfuly.";
            }
            else
            {
                return $"Customer details not avialable with {Id}.";
            }
        }
    }
}
