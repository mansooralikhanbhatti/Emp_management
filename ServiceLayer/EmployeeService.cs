using Emp_management.DataLayer.DTOs;
using Emp_management.DataLayer.Models;
using Emp_management.DataLayer;
using Emp_management.Data;

namespace Emp_management.ServiceLayer
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Employee> GetEmployees(string name, DateTime? doj, string email, EmployeeStatus? status)
        {
            var query = _repository.GetAllEmployees();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (doj.HasValue)
                query = query.Where(e => e.DoJ == DateOnly.FromDateTime(doj.Value.Date)); // Convert DateTime to DateOnly

            if (!string.IsNullOrEmpty(email))
                query = query.Where(e => e.Email.Contains(email));

            if (status.HasValue)
                query = query.Where(e => e.Status == (int)status);

            return query.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _repository.GetEmployeeById(id);
        }

        public void CreateEmployee(EmployeeCreateDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Age = dto.Age,
                Email = dto.Email,
                DoJ = DateOnly.FromDateTime(dto.DoJ),
                Status = (int)dto.Status
            };

            _repository.AddEmployee(employee);
        }

        public void UpdateEmployee(EmployeeUpdateDto dto)
        {
            var employee = _repository.GetEmployeeById(dto.EmpId);
            if (employee != null)
            {
                employee.Name = dto.Name;
                employee.Age = dto.Age;
                employee.Email = dto.Email;

                _repository.UpdateEmployee(employee);
            }
        }

        public void DeleteEmployee(int id)
        {
            _repository.SoftDeleteEmployee(id);
        }
    }
}
