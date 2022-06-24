using accountService.database;
using accountService.database.Entity;
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

        [HttpGet("getCustomerAccountDetails/{CustomerId}")]
        public async Task<ActionResult<Account>> GetCustomerAccountDetails(int CustomerId)
        {
            try
            {
                var accountDetails = db.Accounts.FirstOrDefault(p => p.customerId == CustomerId); 
                if(accountDetails == null)
                {
                    return Ok(new {message= "customer not found" });
                }
                return Ok(new {
                    message = " customer found",
                    data = accountDetails
                });
            }
            catch(Exception e)
            {
                return BadRequest("get customer account api error : "+e);
            }
        }

        [HttpGet("getAccountStatement/{accountId}")]
        public async Task<ActionResult<JsonResult>> GetAccountStatement(int accountId)
        {
 
            try
            {
                var transactionDetails = db.transactions.Where(p => p.CustomerId == accountId);
                if(transactionDetails == null)
                {
                    return Ok(new { messsage = "no transaction found" });
                }
                return Ok(new { message = "transaction fetched", data = transactionDetails });
            }
            catch (Exception e)
            {
                return BadRequest("get customer account api error : " + e);
            }
        }

        [HttpPost("withdraw/{CustomerId}")]
        public async Task<IActionResult> withdraw(int CustomerId, Amount request)
        {
            try
            {
                var customerDetails = db.Accounts.FirstOrDefault(p => p.AccountId == CustomerId);
                customerDetails.totalAmount = customerDetails.totalAmount - request.AmountDC;
                db.SaveChanges();
                return Ok(new { message = "Amount withdraw", value = customerDetails
                });
            }
            catch (Exception e)
            {
                return BadRequest("get customer account api error : " + e);
            }
        }

        [HttpPost("deposit/{CustomerId}")]
        public async Task<IActionResult> deposit(int CustomerId, Amount request)
        {
            //customerAccount res = null;
           
            try
            {
                var customerDetails = db.Accounts.FirstOrDefault(p => p.AccountId == CustomerId);
                customerDetails.totalAmount = customerDetails.totalAmount + request.AmountDC;
                db.SaveChanges();
                return Ok(new
                {
                    message = "Amount Deposit",
                    value = customerDetails
                });
            }
            catch (Exception e)
            {
                return BadRequest("get customer account api error : " + e);
            }
        }
      
    }
}
