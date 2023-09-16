using AccountMemo_Domain.Services;
using AccountMemo_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_EFCore.Services
{
    public class GenericServices<T> : IDataService<T>  where T : BaseModel 
    {
        private AccountMemoContextFactory _contextFactory;
        public GenericServices(AccountMemoContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public virtual async Task<T> Create(T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                EntityEntry createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return (T)createdResult.Entity;
            }

        }
        public virtual async Task<bool> Update(int id, T entity)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    entity.Id = id;
                    context.Set<T>().Update(entity);
                    await context.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                bool Success = false;
                using (var context = _contextFactory.CreateDbContext())
                {
                    T entity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                    context.Set<T>().Remove(entity);
                    await context.SaveChangesAsync();
                    Success = true;
                    return Success;
                }
            }
            catch (Exception)
            {
                return false;
            } 
        }
    }
}
