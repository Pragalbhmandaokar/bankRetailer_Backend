using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using transactionService.database.Entity;

namespace transactionService.database
{
    public class databaseController : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> accounts { get; set; }

        public DbSet<Customer> customers{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"data source=(localDB)\MSSQLLocalDB;initial catalog=BankRetailDB;persist security info=True;");
        }
    }
}
