using Emp_management.Data;
using Emp_management.DataLayer.Models;

namespace Emp_management.DataLayer
{
    public class EmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return _context.Employees.Where(e => e.IsDeleted.HasValue && !e.IsDeleted.Value);
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.EmpId == id && e.IsDeleted.HasValue && !e.IsDeleted.Value);
        }


        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void SoftDeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmpId == id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                _context.SaveChanges();
            }
        }
    }
}
