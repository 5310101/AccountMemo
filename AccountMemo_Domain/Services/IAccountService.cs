using AccountMemo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Domain.Services
{
    public interface IAccountService : IDataService<Account>
    {
        Task<IEnumerable<Account>> GetAllAccount(int userId);
        Task<Account> GetAccount(int userId);   
        Task<IEnumerable<Account>> GetAccountByName(string Name);
        public Task<bool> CreateAccountOfUser(int userId, Account account);
    }
}
