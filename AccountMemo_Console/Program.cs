using System;
using AccountMemo_Console.Services;
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
            IConfiguration configuration = BuildConfiguration();
            var connectionString = configuration.GetConnectionString("connectStr");
            IServiceProvider services = CreateServiceProvider();
            
            try
            {
                ("=========== ACCOUNT MEMO ===========").Title_Display();
                Console.WriteLine();
                while (true)
                {
                    "Command>>".Title_Display();
                    var command = Console.ReadLine();  
                    switch(command.ToLower())
                    {
                        case "user_all":
                            
                            break;
                        case "":

                            break;
                        case "exit":
                            return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

       
        public static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDataService, GenericServices>();
            services.AddSingleton<IUserService, UserService>();

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