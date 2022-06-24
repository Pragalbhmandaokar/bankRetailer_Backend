using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ruleservices.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ruleservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        databaseController db;
        public RoleController()
        {
            db = new databaseController();
        }

        [HttpGet("{AccountId}")]
        public async Task<ActionResult<JsonResult>> Get(int AccountId)
        {
            var accountDetails = db.Accounts.FirstOrDefault(p => p.AccountId == AccountId);
            if(accountDetails.totalAmount < 1000)
            {
                return Ok(new { message = "Evaluate minimum balance" });
            }
            else
            {
                return Ok(new { message = "Healthy Account balance" });
            }
        }

        [HttpGet("/charge/{AccountId}")]
        public async Task<ActionResult<int>> GetServiceCharge(int AccountId)
        {
            var accountDetails = db.Accounts.FirstOrDefault(p => p.AccountId == AccountId);
            if (accountDetails.AccountType == "Savings" || accountDetails.AccountType == "savings")
            {
                return Ok(new { message = "Atm charge", data = 100});
            }
            else if (accountDetails.AccountType == "Current" || accountDetails.AccountType == "Current")
            {
                return Ok(new { message = "Atm charge", data = 100 });
            }
            else
            {
                return 0;
            }
        }
    }
}
