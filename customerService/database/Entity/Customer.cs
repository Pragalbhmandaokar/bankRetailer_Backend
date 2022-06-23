using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customerService.database.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public int AccountNumber { get; set; }

        public DateTime DOB { get; set; }
        public string panNumber { get; set; }

    }
}
