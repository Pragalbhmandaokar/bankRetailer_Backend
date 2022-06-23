using apiGate.database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGate.database
{
    public class databaseController : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Authentication> Authentications { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"data source=(localDB)\MSSQLLocalDB;initial catalog=BankRetailDB;persist security info=True;");

        }
    }
}
