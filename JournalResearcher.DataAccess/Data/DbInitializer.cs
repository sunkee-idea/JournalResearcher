using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.DataAccess.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace JournalResearcher.DataAccess.Data
{
    public class DbInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            InitializeRole(serviceProvider).Wait();
            CreateSuperAdmin(serviceProvider).Wait();

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

        public static async Task CreateSuperAdmin(IServiceProvider serviceProvider)
        {


            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //            if (await userManager.Users.AnyAsync())
            //            {
            //                return;
            //            }
            var login = new RegisterViewModel
            {

                FirstName = "Super",
                LastName = "Admin",
                Email = "superadmin@silveredge.com",
                Password = "Password1!",
                ConfirmPassword = "Password1!",
                Role = "Admin",
                PhoneNumber = "070xxxxxxxxx"



            };



            var newuser = new ApplicationUser
            {
                UserName = login.Email,
                Role = login.Role,
                PasswordHash = login.Password,
                FirstName = login.FirstName,
                LastName = login.LastName,
                Email = login.Email,
                PhoneNumber = login.PhoneNumber,



            };




            var userId = await userManager.Users.SingleOrDefaultAsync(x => x.Email == newuser.Email);

            if (userId == null)
            {
                var createuser = await userManager.CreateAsync(newuser, login.Password);
                if (createuser.Succeeded)
                    if (!await userManager.IsInRoleAsync(newuser, login.Role))
                    {
                        await userManager.AddToRoleAsync(newuser, login.Role);
                    }
            }
        }



    }
}
