using System;
using System.IO.Pipes;
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
            IUserService userService = services.GetRequiredService<IUserService>();
            try
            {
                ("=========== ACCOUNT MEMO ===========").Title_Display();
                Console.WriteLine();
                Task.Run(async () =>
                {
                    while (true)
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
                                if(isSuccess == false)
                                {
                                    "Cannot create user.".Error_Display();
                                }
                                break;
                            case "user_updatebyid":
                                Console.Write("id=");
                                id = int.Parse(Console.ReadLine());
                                user = await library.GetUserToUpdate(id);
                                UserStore userNew = CreateUpdateUser(user);
                                bool Success = await library.UpdateUser(id,userNew);
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

                            case "user_Delete":
                                Console.Write("id=");
                                id = int.Parse(Console.ReadLine());
                                $"Are you sure to delete this user?".Error_Display();
                                string answer = Console.ReadLine(); 
                                if(answer.ToLower() == "y" || answer.ToLower() == "yes")
                                {
                                    Success = await library.DeleteUser(id);
                                    StaticMethod.Display_ToUser(InfoType.DeleteFunction, Success);
                                }
                                if (answer.ToLower().Trim() == "n" || answer.ToLower().Trim() == "no")
                                {
                                    continue;
                                }
                                break;
                            case "exit":
                                return;
                        }
                    }
                }).GetAwaiter().GetResult();    
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".Error_Display();
            }
            
        }

        public static UserStore CreateUpdateUser(UserStore user)
        {
            "Name: ".InputField_Display();
            user.Name =(!string.IsNullOrWhiteSpace(Console.ReadLine())) ? Console.ReadLine() : user.Name;
            "Age: ".InputField_Display();
            user.Age = (!string.IsNullOrWhiteSpace(Console.ReadLine())) ? int.Parse(Console.ReadLine()) : user.Age; 
            "Password: ".InputField_Display();
            user.AppPassword = (!string.IsNullOrWhiteSpace(Console.ReadLine())) ? Console.ReadLine() : user.AppPassword;
            return user;
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