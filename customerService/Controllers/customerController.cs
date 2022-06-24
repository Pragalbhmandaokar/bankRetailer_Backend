using customerService.database;
using customerService.database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerController : ControllerBase
    {
        databaseController db;

        public customerController()
        {
            db = new databaseController();
        }
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return db.customers.ToList();
        }

       

        //public int CustomerId { get; set; }
        //public string Name { get; set; }
        //public string Address { get; set; }
        //public int PhoneNumber { get; set; }
        //public int AccountNumber { get; set; }
        //public DateTime DOB { get; set; }
        //public string panNumber { get; set; }

        [HttpPost("addCustomer")]
        public async Task<ActionResult<Customer>> addCustomer(Customer request)
        {
            if(request.panNumber == null)
            {
                return BadRequest("pan number is empty");
            }
            try
            {
                var get_user = db.customers.FirstOrDefault(p => p.panNumber == request.panNumber);
                if(get_user == null)
                {
                    db.customers.Add(request);
                    db.SaveChanges();
                    return Ok("Customer added successfully");
                }
                else
                {
                    return BadRequest("Customer already exsits");
                }
            }catch(Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> getCustomerDetailsById(int customerId)
        {
            try
            {
                var get_user = db.customers.FirstOrDefault(p => p.CustomerId == customerId);
                if (get_user != null)
                {
                    return Ok(new {message=  "Customer Found",details = get_user });
                }
                else
                {
                    return BadRequest("Customer Not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

    }
}
