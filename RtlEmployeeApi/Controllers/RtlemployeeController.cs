using Microsoft.AspNetCore.Mvc;
using RtlEmployeeApi.Data;
using RtlEmployeeApi.Models;
using RtlEmployeeApi.Models.Entities;

namespace RtlEmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RtlemployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public RtlemployeeController(ApplicationDbContext dbContext)
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
                Salary = addEmployeeDto.Salary,

            };



            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateEmployee(int id, updateEmployeeDto updateEmployeeDto)
        {
            var employeeEntity = dbContext.Employees.Find(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            employeeEntity.Name = updateEmployeeDto.Name;
            employeeEntity.Email = updateEmployeeDto.Email;
            employeeEntity.Phone = updateEmployeeDto.Phone;
            employeeEntity.Salary = updateEmployeeDto.Salary;

            dbContext.Employees.Update(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeEntity = dbContext.Employees.Find(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }





    }
}
