using EmployeeCrudCore.Models;
using EmployeeCrudCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeCrudCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<List<EmployeeTable>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTable>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // GET: api/employee/department/{department}
        [HttpGet("department/{department}")]
        public async Task<ActionResult<List<EmployeeTable>>> GetEmployeesByDepartment(string department)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentAsync(department);
            return Ok(employees);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<EmployeeTable>> CreateEmployee([FromBody] EmployeeTable employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is required.");
            }

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);

            // Returns 201 Created with the location header
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeId }, createdEmployee);
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeTable employee)
        {
            if (id != employee.EmployeeId)
                return BadRequest("Employee ID mismatch.");

            var success = await _employeeService.UpdateEmployeeAsync(employee);

            if (!success)
                return NotFound();

            return NoContent(); // 204 Success, no content returned
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);

            if (!deleted)
                return NotFound(); // Employee not found

            return NoContent(); // 204 success, nothing returned
        }
    }
}
