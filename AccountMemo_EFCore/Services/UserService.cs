using AccountMemo_EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore.Services
{
    public class UserService : GenericServices<UserStore>
    {
        private readonly AccountMemoContextFactory _contextFactory;
        public UserService(AccountMemoContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<UserStore> Get(int Id)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                UserStore entity = await context.UserStores.FirstOrDefaultAsync(x => x.Id == Id);    
                return entity;
            }
        }

        public async Task<IEnumerable<UserStore>> GetAll()
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<UserStore> entity = await context.UserStores.
                    Include(x => x.Id).
                    Include(x => x.Name).
                    Include(x => x.Accounts).ToListAsync(); 
                return entity;  
            }
        }
    }
}
