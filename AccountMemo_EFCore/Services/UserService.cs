using AccountMemo_Domain.Models;
using Microsoft.EntityFrameworkCore;
using AccountMemo_Domain.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore.Services
{
    public class UserService : GenericServices<UserStore>, IUserService
    {
        private readonly AccountMemoContextFactory _contextFactory;
        public UserService(AccountMemoContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<UserStore> GetUser(int Id)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                UserStore entity = await context.UserStores.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == Id);
                return entity;
            }
        }

        public async Task<IEnumerable<UserStore>> GetAllUser()
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<UserStore> entity = await context.UserStores.
                    Include(x => x.Accounts).ToListAsync(); 
                return entity;  
            }
        }

        public async Task<IEnumerable<UserStore>> GetUserByName(string name)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<UserStore> entity = await context.UserStores.Where(x => x.Name == name).
                    Include(x => x.Accounts).ToListAsync();
                return entity;
            }
        }
    }
}
