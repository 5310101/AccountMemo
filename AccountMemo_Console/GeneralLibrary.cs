using AccountMemo_EFCore.Models;
using AccountMemo_EFCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Console
{
    public class GeneralLibrary
    {
        private readonly UserService _userService;  
        private readonly AccountService _accountService;
        public GeneralLibrary(UserService userService, AccountService accountService)
        {
            _userService = userService; 
            _accountService = accountService;
        }
        
        public async Task ShowAllUser()
        {
            try
            {
                IEnumerable<UserStore> listUser = await _userService.GetAll();
                "List User: ".Title_Display();
                foreach (UserStore userStore in listUser)
                {
                    $"[Id{userStore.Id}]  Name: [{userStore.Name}]  Age:[{userStore.Age}]".Normal_Display();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
