using System;
using AccountMemo_EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;

namespace AccountMemo_Console
    
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            IConfiguration configuration = BuildConfiguration();
            var connectionString = configuration.GetConnectionString("connectStr");
            GeneralLibrary library = new GeneralLibrary();
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