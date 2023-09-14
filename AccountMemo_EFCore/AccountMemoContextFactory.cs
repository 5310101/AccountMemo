using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore
{
    public class AccountMemoContextFactory
    {
        private readonly string connectionString;
       public AccountMemoContextFactory(string ConnectionString)
       {
            connectionString = ConnectionString;    
       }

        public AccountMemoContext CreateDbContext() 
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder<AccountMemoContext>();
            options.UseSqlServer(connectionString);
            return new AccountMemoContext(options.Options);
        }
    }
}
