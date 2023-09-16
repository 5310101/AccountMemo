using AccountMemo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Console.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccount(int userId);
        Task<Account> GetAccountById(int AccountId);
        Task<IEnumerable<Account>> GetAccountByName(string Name);
    }
}
