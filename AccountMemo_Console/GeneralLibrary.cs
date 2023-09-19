using AccountMemo_Domain.Models;
using AccountMemo_Domain.Services;
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
        private readonly IUserService _userService;  
        private readonly IAccountService _accountService;
        public GeneralLibrary(IUserService userService, IAccountService accountService)
        {
            _userService = userService; 
            _accountService = accountService;
        }
        
        public async Task ShowAllUser()
        {
            try
            {
                IEnumerable<UserStore> listUser = await _userService.GetAllUser();
                "List User: ".Title_Display();
                foreach (UserStore userStore in listUser)
                {
                    $"[{userStore.Id}]  Name: [{userStore.Name}]  Age:[{userStore.Age}]".Normal_Display();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserStore> GetUserToUpdate(int id)
        {
            UserStore user = await _userService.GetUser(id);
            return user;
        }

        public async Task ShowUser(int id)
        {
            try
            {
                UserStore user = await _userService.GetUser(id);
                if (user == null)
                {
                    "Not existed user".Error_Display();
                }
                else
                {
                    $"[{user.Id}] {user.Name}  {user.Age}".Normal_Display();
                    if (user.Accounts == null)
                    {
                        return;
                    }
                    foreach (Account account in user.Accounts)
                    {
                        $"[{account.Id}] User name: {account.Username}  Password: {account.Password}  Account Type: {account.AccountType}".Normal_Display();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateUser(UserStore user)
        {
            UserStore userCreated = await _userService.Create(user);  
            if(userCreated == null) { return false; }
            else
            {
                "User Created: ".Normal_Display();  
                $"[{userCreated.Id}] {userCreated.Name}  {userCreated.Age}".Normal_Display();
                return true;
            }
        }

        public async Task<bool> UpdateUser(int id, UserStore userUpdate)
        {
                return await _userService.Update(id, userUpdate);
        }

        public async Task DisplayUserByName(string name)
        {
            IEnumerable<UserStore> users = await _userService.GetUserByName(name);
            "Get user Id:".Title_Display();
            foreach (UserStore user in users)
            {
                $"[{user.Id}]  Name: [{user.Name}]  Age:[{user.Age}]".Normal_Display();
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userService.Delete(id);   
        }

        public async Task ShowAllAccountOfUser(int userid)
        {
            IEnumerable<Account> accounts = await _accountService.GetAllAccount(userid);
            if(accounts.Count() == 0)
            {
                $"User Id {userid} cannot be found or has no account.".Error_Display();
                return;
            }
            else
            {
                foreach (Account account in accounts) 
                {
                    $"[{account.Id}] {account.Username}  {account.AccountType} {account.Password}".Normal_Display();
                }
            }
        }

        public async Task<bool> CreateAccount(int userId, Account account)
        {
            try
            {
                return await _accountService.CreateAccountOfUser(userId, account);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task ShowAccount(int accountId)
        {
            Account account = await _accountService.GetAccount(accountId);
            $"[{account.Id} {account.Username}  {account.AccountType}]".Normal_Display();
        }

        public async Task<bool> DeleteAccount(int id)
        {
            bool isSuccess = await _accountService.Delete(id);
            return isSuccess;
        }

        public async Task<Account> GetAccountToUpdate(int id)
        {
            return await _accountService.GetAccount(id);
        }

        public async Task<bool> UpdateAccount(int id, Account accountNew)
        {
            return await _accountService.Update(id,accountNew);
        }
    }
}
