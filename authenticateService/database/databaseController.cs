using authenticateService.database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authenticateService.database
{
    public class databaseController : DbContext
    {
        public DbSet<Authentication> Authentications { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"data source=(localDB)\MSSQLLocalDB;initial catalog=BankRetailDB;persist security info=True;");

        }
    }
}
