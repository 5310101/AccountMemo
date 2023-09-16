using AccountMemo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Console.Services
{
    public interface IUserService  
    {
        Task<UserStore> GetUser(int Id);
        Task<IEnumerable<UserStore>> GetAllUser();

    }
}
