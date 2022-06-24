using accountService.database;
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

        [HttpGet("getCustomerAccountDetails/{CustomerId}",Name ="Get")]
        public async Task<ActionResult<Account>> getCustomerAccount(int CustomerId,Account request)
        {
            try
            {
                var accountDetails = db.Accounts.FirstOrDefault(p => p.AccountId == CustomerId); 
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

        [HttpGet("getAccountStatement/{accountId}", Name = "Get")]
        public async Task<ActionResult<Account>> getAccountStatement(int accountId)
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

        [HttpPost("withdraw/{CustomerId}", Name = "Post")]
        public async Task<ActionResult<Account>> withdrawByCustomerId(int CustomerId, int withdrawalAmount)
        {
            try
            {
                var customerDetails = db.Accounts.FirstOrDefault(p => p.AccountId == CustomerId);
                customerDetails.totalAmount = customerDetails.totalAmount - withdrawalAmount;
                db.SaveChanges();
                return Ok(new {
                    message="Amount withdraw",
                    value=customerDetails.totalAmount
                });
            }
            catch (Exception e)
            {
                return BadRequest("get customer account api error : " + e);
            }
        }

        [HttpPost("deposit/{CustomerId}", Name = "Post")]
        public async Task<ActionResult<Account>> depositByCustomerId(int CustomerId, int withdrawalAmount)
        {
            //customerAccount res = null;
            try
            {
                var customerDetails = db.Accounts.FirstOrDefault(p => p.AccountId == CustomerId);
                customerDetails.totalAmount = customerDetails.totalAmount + withdrawalAmount;
                db.SaveChanges();
                return Ok(new
                {
                    message = "Amount Deposit",
                    value = customerDetails.totalAmount
                });
            }
            catch (Exception e)
            {
                return BadRequest("get customer account api error : " + e);
            }
        }
      
    }
}
