using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using transactionService.database;
using transactionService.database.Entity;

namespace transactionService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TransactionController : ControllerBase
    {
        databaseController db;
        public TransactionController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IEnumerable<database.Entity.Transaction> Get()
        {
            return db.Transactions.ToList();
        }

        [HttpPost("addTransaction")]
        public async Task<ActionResult<database.Entity.Transaction>> Register(database.Entity.Transaction request)
        {
            try
            {
                var get_customer = db.customers.FirstOrDefault(p => p.CustomerId == request.CustomerId);
                var SenderCustomer = db.accounts.FirstOrDefault(p => p.AccountId == request.SourceAccountID);
                var recieverCustomer = db.accounts.FirstOrDefault(p => p.AccountId == request.TargetAccountID);

                if(SenderCustomer == null)
                {
                    return Ok("sender not found");
                }
                if (recieverCustomer == null)
                {
                    return Ok("reciever not found");
                };
                if (get_customer != null)
                {
                    recieverCustomer.totalAmount += request.CreditAmount;
                    SenderCustomer.totalAmount -= request.CreditAmount;
                    db.Transactions.Add(request);

                    db.SaveChanges();
                    return Ok("Transaction Completed");
                }
                else
                {
                    return BadRequest("Customer not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error while doing transaction" + e);
            }
        }

        [HttpGet("getStatement/{customerId}")]
        public async Task<ActionResult<JsonResult>> GetCustomerBankStatement(int customerId)
        {
            try
            {
                var get_AccountStatement = db.Transactions.Where(p => p.CustomerId == customerId).ToList();
                if (get_AccountStatement != null)
                {

                    return Ok(new
                    {
                        message ="",
                        data = get_AccountStatement
                    });
                }
                else
                {
                    return BadRequest("Customer not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error while doing transaction" + e);
            }
        }

        [HttpPost("deposit")]
        public async Task<ActionResult<database.Entity.Transaction>> depositByCustomerId(database.Entity.Transaction request)
        {
            try
            {
                var TransactionDetails = db.Transactions.FirstOrDefault(p => p.CustomerId == request.CustomerId);
                var accountDetails = db.accounts.FirstOrDefault(p => p.customerId == request.CustomerId);
                if(accountDetails == null)
                {
                    return BadRequest("Account not found");
                }
                db.Transactions.Add(request);
                accountDetails.totalAmount += request.DepositAmount;
                db.SaveChanges();
                return Ok(new
                {
                    message = "Amount Deposit",
                    amount = accountDetails.totalAmount,
                    transactionId = TransactionDetails.TransactionID
                }); ;
            }
            catch (Exception e)
            {
                return BadRequest("get customer account api error : " + e);
            }
        }
    }
}
