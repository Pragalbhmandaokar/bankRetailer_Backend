using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGate.database.Entity
{
    public class Account
    {
        public int AccountId{ get; set; }
        public string AccountType { get; set; }
        public string chqNumber { get; set; }
        public DateTime OpenningDate { get; set; }
        public int totalAmount { get; set; }
        public int customerId { get; set; }
        public int AccountStatus { get; set; }

    }
}
