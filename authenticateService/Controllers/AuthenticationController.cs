using authenticateService.database;
using authenticateService.database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authenticateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        databaseController db;
        public AuthenticationController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IEnumerable<Authentication> Get()
        {
            return db.Authentications.ToList();
        }

        [HttpPost("register")]
        public async Task<ActionResult<Authentication>> Register(Authentication request)
        {
            try
            {
                var get_user = db.Authentications.FirstOrDefault(p => p.Username == request.Username);
                if(get_user == null)
                {
                    db.Authentications.Add(request);
                    db.SaveChanges();
                    return Ok("Register Successful");
                }
                else
                {
                    return BadRequest("Username already exsits");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }

        }
        [HttpPost("login")]

        public async Task<ActionResult<Authentication>> login(Authentication request)
        {
            //CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            try
            {
                var get_user = db.Authentications.FirstOrDefault(p => p.Username == request.Username && p.Password == request.Password);
                if(get_user != null)
                {
                    return Ok("login successfull");
                }
                else
                {
                  
                    return BadRequest("UserName or Password does not match.");
                }
                db.SaveChanges();
                return Ok("Register Successful");
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }

        }
    }
}
