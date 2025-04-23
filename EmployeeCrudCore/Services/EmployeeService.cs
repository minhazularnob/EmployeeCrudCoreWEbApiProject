using EmployeeCrudCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudCore.Services
{
    public class EmployeeService
    {
        private readonly EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext context)
        {
            _context = context;
        }

        // Get all employees
        public async Task<List<EmployeeTable>> GetAllEmployeesAsync()
        {
            return await _context.EmployeeTables.ToListAsync();
        }

        // Get employee by ID
        public async Task<EmployeeTable?> GetEmployeeByIdAsync(int id)
        {
            return await _context.EmployeeTables
                                 .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        // Get employees by department
        public async Task<List<EmployeeTable>> GetEmployeesByDepartmentAsync(string department)
        {
            return await _context.EmployeeTables
                                 .Where(e => e.Department == department)
                                 .ToListAsync();
        }

        // Create a new employee
        public async Task<EmployeeTable> CreateEmployeeAsync(EmployeeTable employee)
        {
            _context.EmployeeTables.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        // Update existing employee
        public async Task<bool> UpdateEmployeeAsync(EmployeeTable updatedEmployee)
        {
            var existingEmployee = await _context.EmployeeTables
                                                 .FirstOrDefaultAsync(e => e.EmployeeId == updatedEmployee.EmployeeId);

            if (existingEmployee == null)
                return false;

            // Update fields
            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.LastName = updatedEmployee.LastName;
            existingEmployee.Email = updatedEmployee.Email;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.HireDate = updatedEmployee.HireDate;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete an employee by ID
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.EmployeeTables
                                         .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return false;

            _context.EmployeeTables.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
