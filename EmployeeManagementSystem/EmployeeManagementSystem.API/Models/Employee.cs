using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.API.Models
{
    public enum EmploymentStatus
    {
        Active,
        Inactive,
        Resigned
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [StringLength(250)]
        public string Address { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;

        [StringLength(250)]
        public string? ProfileImagePath { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int DesignationId { get; set; }
        public Designation? Designation { get; set; }
    }
}