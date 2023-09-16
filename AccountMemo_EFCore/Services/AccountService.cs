using AccountMemo_Domain.Models;
using AccountMemo_Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore.Services
{
    public class AccountService : GenericServices<Account>, IAccountService
    {
        private AccountMemoContextFactory _contextFactory;
       
        public AccountService(AccountMemoContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Account>> GetAllAccount(int userId)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                UserStore? user = await context.UserStores
                    .Include(a => a.Accounts)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user?.Accounts == null)
                {
                    return Enumerable.Empty<Account>();
                }
                return user.Accounts.ToList();
            }
        }


        public async Task<Account> GetAccountById(int AccountId)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                Account? account = await context.Accounts.FirstOrDefaultAsync(a => a.Id == AccountId);
                return account;
            }
        }

        public async Task<IEnumerable<Account>> GetAccountByName(string Name)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                List<Account> accounts = await context.UserStores
                    .Where(x => x.Name == Name)
                    .SelectMany(a => a.Accounts)
                    .ToListAsync();
                return accounts ?? Enumerable.Empty<Account>();    
            }
        }
    }
}
