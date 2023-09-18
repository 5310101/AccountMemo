using AccountMemo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Domain.Services
{
    public interface IUserService : IDataService<UserStore>
    {
        public Task<UserStore> GetUser(int Id);
        public Task<IEnumerable<UserStore>> GetAllUser();
        public Task<IEnumerable<UserStore>> GetUserByName(string name);

    }
}
