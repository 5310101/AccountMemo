using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Domain.Services
{
    public interface IDataService<T>
    {
        Task<T> Create(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
