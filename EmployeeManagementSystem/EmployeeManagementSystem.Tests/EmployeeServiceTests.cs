using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.API.Models;
using EmployeeManagementSystem.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagementSystem.Tests
{
    public class EmployeeServiceTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Departments.Add(new Department { Id = 1, Name = "Engineering", Code = "ENG" });
            context.Designations.Add(new Designation { Id = 1, Title = "Software Engineer" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task CreateEmployeeAsync_ValidDto_CreatesEmployeeSuccessfully()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new EmployeeService(context);

            var dto = new CreateEmployeeDto
            {
                EmployeeCode = "EMP-999",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@ems.com",
                Phone = "+1-555-9999",
                DateOfBirth = new DateTime(1995, 5, 15),
                Gender = Gender.Female,
                Address = "123 Test St",
                Salary = 85000,
                Status = EmploymentStatus.Active,
                DepartmentId = 1,
                DesignationId = 1
            };

            // Act
            var result = await service.CreateEmployeeAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("EMP-999", result.EmployeeCode);
            Assert.Equal("Jane Doe", result.FullName);
        }

        [Fact]
        public async Task GetEmployeesAsync_ReturnsPagedList()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new EmployeeService(context);

            await service.CreateEmployeeAsync(new CreateEmployeeDto
            {
                EmployeeCode = "EMP-001",
                FirstName = "John",
                LastName = "Smith",
                Email = "john@ems.com",
                Phone = "123456",
                DepartmentId = 1,
                DesignationId = 1
            });

            // Act
            var (items, totalCount) = await service.GetEmployeesAsync(null, null, null, 1, 10);

            // Assert
            Assert.Equal(1, totalCount);
            Assert.Single(items);
        }
    }
}