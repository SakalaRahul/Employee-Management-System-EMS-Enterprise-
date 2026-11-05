using EmployeeManagementSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.API.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ProfileImagePath { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int DesignationId { get; set; }
        public string DesignationTitle { get; set; } = string.Empty;
    }

    public class CreateEmployeeDto
    {
        [Required, StringLength(20)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;

        [Range(0, 10000000)]
        public decimal Salary { get; set; }
        public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;

        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
    }

    public class UpdateEmployeeDto : CreateEmployeeDto
    {
    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class CreateDepartmentDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }
    }
}