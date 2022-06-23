using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authenticateService.database.Entity
{
    public class Authentication
    {
        public int AuthenticationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
