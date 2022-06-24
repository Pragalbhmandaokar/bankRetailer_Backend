using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ruleModule.database
{
    public class databaseController : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"data source=(localDB)\MSSQLLocalDB;initial catalog=BankRetailDB;persist security info=True;");

        }
    }
}
