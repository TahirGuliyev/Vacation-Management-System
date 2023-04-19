using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vacation.Models;

namespace Vacation.Data
{
    public class DataSeeder
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<VacationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                var roles = new Role[] { new Role { Name = "HR" }, new Role { Name = "Employee" } };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!context.Users.Any())
            {
                // Create HR User
                var user = new Account
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User"
                };

                var userResult = await userManager.CreateAsync(user, "Admin123!");
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "HR");
                }
                else
                {
                    foreach (var error in userResult.Errors)
                    {
                        Console.WriteLine($"User Error: {error.Description}");
                    }
                }

                // Create Employee User
                var user2 = new Account
                {
                    UserName = "employee",
                    Email = "employee@example.com",
                    FirstName = "Employee",
                    LastName = "User"
                };

                var employeeResult = await userManager.CreateAsync(user2, "Employee123!");
                if (employeeResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "Employee");
                }
                else
                {
                    foreach (var error in employeeResult.Errors)
                    {
                        Console.WriteLine($"Employee Error: {error.Description}");
                    }
                }
                await context.SaveChangesAsync();

                var createdHrUser = context.Users.Single(u => u.Email == user.Email);
                var createdEmployeeUser = context.Users.Single(u => u.Email == user2.Email);

                var employee = new Employee
                {
                    AccountId = createdHrUser.Id.ToString()
                };
                context.Employees.Add(employee);

                var employee2 = new Employee
                {
                    AccountId = createdEmployeeUser.Id.ToString()
                };
                context.Employees.Add(employee2);

                await context.SaveChangesAsync();
            }
        }
    }
}
