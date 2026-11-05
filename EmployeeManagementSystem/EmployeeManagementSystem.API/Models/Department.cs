using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.API.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}