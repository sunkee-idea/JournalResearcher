using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JournalResearcher.DataAccess.Data
{
    public class DbInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            InitializeRole(serviceProvider).Wait();
        }

        public static async Task InitializeRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                var isRoleExists = await roleManager.RoleExistsAsync(role);
                if (!isRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
               
            }
        }
    }
}
