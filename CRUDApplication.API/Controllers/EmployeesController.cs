using CRUDApplication.API.Data;
using CRUDApplication.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDApplication.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly CRUDApplicationDbContext _crudAppDbContext;

        public EmployeesController(CRUDApplicationDbContext crudAppDbContext)
        {
            _crudAppDbContext = crudAppDbContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _crudAppDbContext.Employee.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _crudAppDbContext.Employee.AddAsync(employeeRequest);
            await _crudAppDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute]Guid id)
        {
            var employee = await _crudAppDbContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee employeeRequest)
        {
            var employee = await _crudAppDbContext.Employee.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = employeeRequest.Name;
            employee.Email = employeeRequest.Email;
            employee.Salary = employeeRequest.Salary;
            employee.Phone = employeeRequest.Phone;
            employee.Department = employeeRequest.Department;
            await _crudAppDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _crudAppDbContext.Employee.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            _crudAppDbContext.Employee.Remove(employee);
            await _crudAppDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        }
}
