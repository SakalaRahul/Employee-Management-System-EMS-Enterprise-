using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<EmployeeDto> Items, int TotalCount)> GetEmployeesAsync(string? search, int? departmentId, string? status, int page = 1, int pageSize = 10)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.FirstName.Contains(search) || e.LastName.Contains(search) || e.EmployeeCode.Contains(search) || e.Email.Contains(search));
            }

            if (departmentId.HasValue && departmentId.Value > 0)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<EmploymentStatus>(status, true, out var parsedStatus))
            {
                query = query.Where(e => e.Status == parsedStatus);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    EmployeeCode = e.EmployeeCode,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender.ToString(),
                    Address = e.Address,
                    JoiningDate = e.JoiningDate,
                    Salary = e.Salary,
                    Status = e.Status.ToString(),
                    ProfileImagePath = e.ProfileImagePath,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.Name : "N/A",
                    DesignationId = e.DesignationId,
                    DesignationTitle = e.Designation != null ? e.Designation.Title : "N/A"
                })
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var e = await _context.Employees
                .Include(emp => emp.Department)
                .Include(emp => emp.Designation)
                .FirstOrDefaultAsync(emp => emp.Id == id);

            if (e == null) return null;

            return new EmployeeDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender.ToString(),
                Address = e.Address,
                JoiningDate = e.JoiningDate,
                Salary = e.Salary,
                Status = e.Status.ToString(),
                ProfileImagePath = e.ProfileImagePath,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department != null ? e.Department.Name : "N/A",
                DesignationId = e.DesignationId,
                DesignationTitle = e.Designation != null ? e.Designation.Title : "N/A"
            };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                EmployeeCode = dto.EmployeeCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                JoiningDate = dto.JoiningDate,
                Salary = dto.Salary,
                Status = dto.Status,
                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return (await GetEmployeeByIdAsync(employee.Id))!;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.EmployeeCode = dto.EmployeeCode;
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.DateOfBirth = dto.DateOfBirth;
            employee.Gender = dto.Gender;
            employee.Address = dto.Address;
            employee.JoiningDate = dto.JoiningDate;
            employee.Salary = dto.Salary;
            employee.Status = dto.Status;
            employee.DepartmentId = dto.DepartmentId;
            employee.DesignationId = dto.DesignationId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Code = d.Code,
                    Description = d.Description,
                    EmployeeCount = d.Employees.Count
                })
                .ToListAsync();
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var d = await _context.Departments
                .Include(dept => dept.Employees)
                .FirstOrDefaultAsync(dept => dept.Id == id);

            if (d == null) return null;

            return new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                EmployeeCount = d.Employees.Count
            };
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            var dept = new Department
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description
            };

            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                Description = dept.Description,
                EmployeeCount = 0
            };
        }
    }
}