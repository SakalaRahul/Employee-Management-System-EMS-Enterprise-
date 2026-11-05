using EmployeeManagementSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "HRManager", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            var adminEmail = "admin@ems.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Departments
            if (!context.Departments.Any())
            {
                var deps = new List<Department>
                {
                    new Department { Name = "Information Technology", Code = "IT", Description = "Software & Infrastructure" },
                    new Department { Name = "Human Resources", Code = "HR", Description = "Talent & Culture" },
                    new Department { Name = "Finance & Accounting", Code = "FIN", Description = "Financial operations" },
                    new Department { Name = "Marketing & Sales", Code = "MKT", Description = "Growth & Customer Outreach" },
                    new Department { Name = "Operations & Logistics", Code = "OPS", Description = "Business logistics & Support" }
                };
                context.Departments.AddRange(deps);
                await context.SaveChangesAsync();
            }

            // Seed Designations
            if (!context.Designations.Any())
            {
                var desigs = new List<Designation>
                {
                    new Designation { Title = "Senior Software Engineer", Responsibilities = "System Architecture & Coding" },
                    new Designation { Title = "HR Manager", Responsibilities = "Employee engagement & Recruitment" },
                    new Designation { Title = "Financial Analyst", Responsibilities = "Budgeting & Financial Reports" },
                    new Designation { Title = "Marketing Executive", Responsibilities = "Campaigns & Sales Lead Management" },
                    new Designation { Title = "Operations Lead", Responsibilities = "Daily operational coordination" }
                };
                context.Designations.AddRange(desigs);
                await context.SaveChangesAsync();
            }

            // Seed 20+ Employees
            if (!context.Employees.Any())
            {
                var itDep = context.Departments.First(d => d.Code == "IT").Id;
                var hrDep = context.Departments.First(d => d.Code == "HR").Id;
                var finDep = context.Departments.First(d => d.Code == "FIN").Id;
                var mktDep = context.Departments.First(d => d.Code == "MKT").Id;
                var opsDep = context.Departments.First(d => d.Code == "OPS").Id;

                var seDes = context.Designations.First(d => d.Title.Contains("Software")).Id;
                var hrDes = context.Designations.First(d => d.Title.Contains("HR")).Id;
                var finDes = context.Designations.First(d => d.Title.Contains("Financial")).Id;
                var mktDes = context.Designations.First(d => d.Title.Contains("Marketing")).Id;
                var opsDes = context.Designations.First(d => d.Title.Contains("Operations")).Id;

                var employees = new List<Employee>();
                for (int i = 1; i <= 20; i++)
                {
                    employees.Add(new Employee
                    {
                        EmployeeCode = $"EMP-{1000 + i}",
                        FirstName = $"FirstName{i}",
                        LastName = $"LastName{i}",
                        Email = $"employee{i}@ems.com",
                        Phone = $"+1-555-010{i:D2}",
                        DateOfBirth = new DateTime(1990, (i % 12) + 1, (i % 28) + 1),
                        Gender = (i % 2 == 0) ? Gender.Male : Gender.Female,
                        Address = $"{i * 12} Tech Boulevard, Suite {i}",
                        JoiningDate = DateTime.UtcNow.AddDays(-i * 30),
                        Salary = 60000m + (i * 1500m),
                        Status = (i % 7 == 0) ? EmploymentStatus.Inactive : EmploymentStatus.Active,
                        DepartmentId = (i % 5 == 0) ? hrDep : (i % 4 == 0) ? finDep : (i % 3 == 0) ? mktDep : (i % 2 == 0) ? opsDep : itDep,
                        DesignationId = (i % 5 == 0) ? hrDes : (i % 4 == 0) ? finDes : (i % 3 == 0) ? mktDes : (i % 2 == 0) ? opsDes : seDes
                    });
                }
                context.Employees.AddRange(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}