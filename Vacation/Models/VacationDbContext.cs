using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vacation.Data;

namespace Vacation.Models;

public class VacationDbContext : IdentityDbContext<Account, Role, int>
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<VacationDays> VacationDays { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<VacationRequest> VacationRequests { get; set; }

    public VacationDbContext(DbContextOptions<VacationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Account>()
        .HasOne(a => a.Employee)
        .WithOne(e => e.Account)
        .HasForeignKey<Account>(a => a.EmployeeID);
        modelBuilder.Entity<Department>().ToTable("Department");
        modelBuilder.Entity<Job>().ToTable("Job");
        modelBuilder.Entity<VacationDays>().ToTable("VacationDays");
        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<VacationRequest>().ToTable("VacationRequest");

        modelBuilder.Entity<Department>().HasData(
            new Department { ID = 1, ShortName = "HR", FullName = "Human Resources", Note = "HR Department" },
            new Department { ID = 2, ShortName = "IT", FullName = "Information Technology", Note = "IT Department" }
        );

        modelBuilder.Entity<Job>().HasData(
            new Job { ID = 1, Name = "HR Manager" },
            new Job { ID = 2, Name = "Developer" }
        );

        modelBuilder.Entity<Employee>().HasData(new Employee
        {
            ID = 1,
            FirstName = "HR",
            LastName = "Manager",
            JobID = 1,
            DepartmentID = 1
        });

        for (int i = 2; i <= 6; i++)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                ID = i,
                FirstName = $"Employee{i}",
                LastName = $"Surname{i}",
                JobID = 2,
                DepartmentID = 2
            });
        }
    }
}
