using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OTASystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTASystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            InitDB(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitDB(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OTADb>();
                    context.Database.Migrate();

                    AddSeedData(services).Wait();
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred createing the DB.");
                    Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                    InitDB(host);
                }
            }
        }

        private static async Task AddSeedData(IServiceProvider services)
        {
            var userManager = services.GetService<UserManager<IdentityUser>>();
            var roleManager = services.GetService<RoleManager<IdentityRole>>();

            // 默认管理员用户
            await EnsureAdminUser(userManager, roleManager, "admin", "123456");
        }

        private static async Task<string> EnsureAdminUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, string userName, string userPassword)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser(userName)
                {
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, userPassword);

                //var adminRole = new IdentityRole("admin");
                //var userRole = new IdentityRole("user");
                //await roleManager.CreateAsync(adminRole);
                //await roleManager.CreateAsync(userRole);
                //await userManager.AddToRoleAsync(user, "admin");
            }

            return user.Id;
        }
    }
}
