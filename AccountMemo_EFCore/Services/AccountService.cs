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

        public async Task<Account> GetAccount(int id)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
                return account;
            }
        }

        public async Task<IEnumerable<Account>> GetAccountByName(string Name)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                List<Account> accounts = await context.UserStores.Include(a => a.Accounts)
                    .Where(x => x.Name == Name)
                    .SelectMany(a => a.Accounts)
                    .ToListAsync();
                return accounts ?? Enumerable.Empty<Account>();
            }
        }

        public async Task<bool> CreateAccountOfUser(int userId, Account account)
        {
            bool isSuccess = false;
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                UserStore? user = await context.UserStores.FirstOrDefaultAsync(x => x.Id == userId);
                if (user == null)
                {
                }
                else
                {
                    user.Accounts.Add(account);
                    await context.SaveChangesAsync();
                    isSuccess = true;
                }
                return isSuccess;
            }
        }
    }
}
