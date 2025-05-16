using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RtlEmployeePortal.Data;
using RtlEmployeePortal.Models;
using RtlEmployeePortal.Models.Entities;

namespace RtlEmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbcontext;

        public employeeController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var allEmployee = dbcontext.Employees.ToList();
            return Ok(allEmployee);
        }

        [HttpPost]
        public IActionResult addEmployee(addEmployeeDto AddEmployeeDto)
        {

            var employeeEntity = new employee()
            {
                name = AddEmployeeDto.name,
                Email = AddEmployeeDto?.Email,
                phone = AddEmployeeDto.phone,
                salary= AddEmployeeDto?.salary,
            };

            dbcontext.Employees.Add(employeeEntity);
            dbcontext.SaveChanges();

            return Ok(employeeEntity);
        }
        
    }

}
