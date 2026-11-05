using EmployeeManagementSystem.API.DTOs;

namespace EmployeeManagementSystem.API.Services
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDto> Items, int TotalCount)> GetEmployeesAsync(string? search, int? departmentId, string? status, int page = 1, int pageSize = 10);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
    }

    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto);
    }
}