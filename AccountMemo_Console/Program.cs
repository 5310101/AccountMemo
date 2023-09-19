using System;
using System.IO.Pipes;
using System.Reflection.Metadata.Ecma335;
using AccountMemo_Domain;
using AccountMemo_Domain.Models;
using AccountMemo_Domain.Services;
using AccountMemo_EFCore;
using AccountMemo_EFCore.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountMemo_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider services = CreateServiceProvider();
            GeneralLibrary library = services.GetRequiredService<GeneralLibrary>();
            Task.Run(async () =>
            {
                ("=========== ACCOUNT MEMO ===========").Title_Display();
                Console.WriteLine();
                while (true)
                {
                    try
                    {
                        "Command>>".Title_Display();
                        var command = Console.ReadLine();
                        switch (command.ToLower())
                        {
                            case "user_all":
                                await library.ShowAllUser();
                                break;
                            case "user_id":
                                Console.Write("id=");
                                int id = int.Parse(Console.ReadLine());
                                await library.ShowUser(id);
                                break;
                            case "user_create":
                                UserStore user = CreateUser();
                                bool isSuccess = await library.CreateUser(user);
                                if (isSuccess == false)
                                {
                                    "Cannot create user.".Error_Display();
                                }
                                break;
                            case "user_updatebyid":
                                Console.Write("id=");
                                id = int.Parse(Console.ReadLine());
                                user = await library.GetUserToUpdate(id);
                                UserStore userNew = CreateUpdateUser(user);
                                bool Success = await library.UpdateUser(id, userNew);
                                StaticMethod.Display_ToUser(InfoType.UpdateFunction, Success);
                                break;
                            case "user_updatebyname":
                                Console.Write("Name=");
                                string name = Console.ReadLine();
                                await library.DisplayUserByName(name);
                                "Insert Id=".Normal_Display();
                                id = int.Parse(Console.ReadLine());
                                user = await library.GetUserToUpdate(id);
                                userNew = CreateUpdateUser(user);
                                Success = await library.UpdateUser(id, userNew);
                                StaticMethod.Display_ToUser(InfoType.UpdateFunction, Success);
                                break;

                            case "user_delete":
                                Console.Write("id=");
                                id = int.Parse(Console.ReadLine());
                                $"Are you sure to delete this user?".Error_Display();
                                string answer = Console.ReadLine();
                                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                                {
                                    Success = await library.DeleteUser(id);
                                    StaticMethod.Display_ToUser(InfoType.DeleteFunction, Success);
                                }
                                if (answer.ToLower().Trim() == "n" || answer.ToLower().Trim() == "no")
                                {
                                    continue;
                                }
                                break;

                            case "account_all":
                                Console.Write("userid=");
                                id = int.Parse(Console.ReadLine());
                                await library.ShowAllAccountOfUser(id);
                                break;
                            case "account_create":
                                Console.Write("userid=");
                                id = int.Parse(Console.ReadLine());
                                await library.ShowUser(id);
                                Account account = CreateInputAccount();
                                Success = await library.CreateAccount(id, account);
                                StaticMethod.Display_ToUser(InfoType.CreateFunction, Success);
                                break;

                            case "account_delete":
                                Console.Write("Account id:");
                                id = int.Parse(Console.ReadLine());
                                await library.ShowAccount(id);
                                "Are you sure to delete this account?".Error_Display();
                                answer = Console.ReadLine();
                                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                                {
                                    Success = await library.DeleteAccount(id);
                                    StaticMethod.Display_ToUser(InfoType.DeleteFunction, Success);
                                }
                                if (answer.ToLower().Trim() == "n" || answer.ToLower().Trim() == "no")
                                {
                                    continue;
                                }
                                break;
                            case "account_update":
                                Console.Write("Account id:");
                                id = int.Parse(Console.ReadLine());
                                account = await library.GetAccountToUpdate(id);
                                Account accountNew = CreateUpdateAccount(account);
                                Success = await library.UpdateAccount(id, accountNew);
                                StaticMethod.Display_ToUser(InfoType.UpdateFunction, Success);
                                break;
                            case "exit":
                                return;
                        }
                    }
                    catch (Exception ex)
                    {
                        $"Error: {ex.Message}".Error_Display();
                    }

                }
            }).GetAwaiter().GetResult();
        }

        private static Account CreateUpdateAccount(Account account)
        {
            try
            {
                "Username: ".InputField_Display();
                string Username = Console.ReadLine();
                account.Username = (!string.IsNullOrWhiteSpace(Username)) ? Username : account.Username;
                
                "Password: ".InputField_Display();
                string Password = Console.ReadLine();
                account.Password = (!string.IsNullOrWhiteSpace(Password)) ? Password : account.Password;

                "Account type: ".InputField_Display();
                string Type = Console.ReadLine();
                account.AccountType = (!string.IsNullOrWhiteSpace(Type)) ? Type : account.AccountType;
                return account;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static Account CreateInputAccount()
        {
            Account account = new Account();
            "Username:".Normal_Display();
            account.Username = Console.ReadLine();
            "Password:".Normal_Display();
            account.Password = Console.ReadLine();
            "Account Type:".Normal_Display();
            account.AccountType = Console.ReadLine();
            return account;
        }

        public static UserStore CreateUpdateUser(UserStore user)
        {
            try
            {
                "Name: ".InputField_Display();
                string Name = Console.ReadLine();
                user.Name = (!string.IsNullOrWhiteSpace(Name)) ? Name : user.Name;
                "Age: ".InputField_Display();
                string Age = Console.ReadLine();
                user.Age = (!string.IsNullOrWhiteSpace(Age)) ? int.Parse(Age) : user.Age;
                "Password: ".InputField_Display();
                string Password = Console.ReadLine();
                user.AppPassword = (!string.IsNullOrWhiteSpace(Password)) ? Password : user.AppPassword;
                return user;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static UserStore CreateUser()
        {
            UserStore userStore = new UserStore();
            "Name: ".InputField_Display();
            userStore.Name = Console.ReadLine();
            "Age: ".InputField_Display();
            userStore.Age = int.Parse(Console.ReadLine());
            "Password: ".InputField_Display();
            userStore.AppPassword = Console.ReadLine();
            return userStore;
        }

        public static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration configuration = BuildConfiguration();
            var connectionString = configuration.GetValue<string>("connectStr");
            services.AddSingleton<AccountMemoContextFactory>(new AccountMemoContextFactory(connectionString));
            services.AddSingleton<IDataService<UserStore>, GenericServices<UserStore>>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IDataService<Account>, GenericServices<Account>>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<GeneralLibrary>(services =>
            {
                return new GeneralLibrary(services.GetRequiredService<IUserService>(), services.GetRequiredService<IAccountService>());
            });

            return services.BuildServiceProvider();
        }

        public static IConfigurationRoot BuildConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }
    }
}