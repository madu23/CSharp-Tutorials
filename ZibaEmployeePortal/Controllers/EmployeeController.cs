using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZibaEmployeePortal.Data;
using ZibaEmployeePortal.Models;
using ZibaEmployeePortal.Models.Entities;

namespace ZibaEmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetEmployeesById(int id)
        {
           var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
            
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {

            var employeeEntity = new Employee()
            {

                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                salary = addEmployeeDto.salary

            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateEmployee(int id,UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;   
            employee.Phone = updateEmployeeDto.Phone;
            employee.salary = updateEmployeeDto.salary;

            dbContext.SaveChanges();

            return Ok(employee);
        }   
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee =dbContext.Employees.Find(id);

            if(employee == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }

    }
}
