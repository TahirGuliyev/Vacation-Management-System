using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vacation.Models;

namespace Vacation.Data;
public class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using (var context = new VacationDbContext(serviceProvider.GetRequiredService<DbContextOptions<VacationDbContext>>()))
        {
            if (context.Users.Any())
            {
                return;
            }

            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<Account>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

                var roles = new List<Role>
                {
                    new Role { Name = "HR" },
                    new Role { Name = "Employee" }
                };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = new Account
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmployeeID = 1,
                    UserRole = "HR"
                };

                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, user.UserRole);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding data: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
