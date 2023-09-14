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
    public class UserService
    {
        private readonly AccountMemoContextFactory _contextFactory;
        public UserService(AccountMemoContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<UserStore> Create(UserStore entity)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                await context.UserStores.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
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

        public async Task Update(int id, UserStore entity)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.UserStores.Update(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (AccountMemoContext context = _contextFactory.CreateDbContext())
            {
                UserStore entity = await context.UserStores.FirstOrDefaultAsync(x => x.Id == id);
                context.UserStores.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
