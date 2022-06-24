using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGate.database.Entity
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int CustomerId { get; set; }
        public int TransactionType { get; set; }
        public int DepositAmount { get; set; }
        public int CreditAmount { get; set; }
        public int SourceAccountID { get; set; }
        public int TargetAccountID { get; set; }
        public int balance { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
