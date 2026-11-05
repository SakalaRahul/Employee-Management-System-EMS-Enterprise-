using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.API.Models
{
    public class Designation
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Responsibilities { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}