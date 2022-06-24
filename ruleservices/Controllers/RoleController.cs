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
            return Ok("Evaluate min balance");
        }

        [HttpGet("/charge/{AccountId}")]
        public async Task<ActionResult<int>> GetServiceCharge(string AccountType)
        {
            if (AccountType == "Savings")
            {
                return 100;
            }
            else if (AccountType == "Current")
            {
                return 200;
            }
            else
            {
                return 0;
            }
        }
    }
}
