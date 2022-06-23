using accountService.database;
using accountService.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace accountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        databaseController db;

        public AccountController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return db.Accounts.ToList();
        }

        [HttpPost("addAccount")]
        public async Task<ActionResult<Account>> AddAccount(Account request)
        {
            try
            {
                db.Accounts.Add(request);
                db.SaveChanges();
                return Ok("Account created");
            }catch(Exception e)
            {
                return BadRequest("Add Account api error :" + e);
            }
        }
        [HttpPost("getCustomerAccount/{CustomerId}",Name ="Post")]
        public async Task<ActionResult<customerAccount>> getCustomerAccount(int CustomerId,Account request)
        {
            customerAccount res = null;
            try
            {
                var customerDetails = db.Accounts.FirstOrDefault(p => p.AccountId == CustomerId); 
                return Ok(res);
            }
            catch(Exception e)
            {
                return BadRequest("get customer account api error : "+e);
            }
            
        }   
    }
}
