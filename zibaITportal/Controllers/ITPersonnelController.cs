using Microsoft.AspNetCore.Mvc;
using zibaITportal.Data;
using zibaITportal.Models;
using zibaITportal.Models.Entities;

namespace zibaITportal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITPersonnelController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ITPersonnelController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllItPersonnel()
        {

            var allITPersonnel = dbContext.itPersonnel.ToList();

            return Ok(allITPersonnel);

        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetItPersonnelById(int id)
        {
            var itPersonnel = dbContext.itPersonnel.Find(id);
            if (itPersonnel == null)
            {
                return NotFound();
            }
            return Ok(itPersonnel);
        }
        [HttpPost]
        public IActionResult AddItPersonnel(AddItPersonnelDto addItPersonnelDto)
        {
            var itPersonnel = new ItPersonnel
            {
                Name = addItPersonnelDto.Name,
                Email = addItPersonnelDto.Email,
                PhoneNumber = addItPersonnelDto.PhoneNumber,
                Department = addItPersonnelDto.Department,
                Position = addItPersonnelDto.Position,
                DateOfBirth = addItPersonnelDto.DateOfBirth,
                DateOfJoining = addItPersonnelDto.DateOfJoining,
                Address = addItPersonnelDto.Address
            };
            dbContext.itPersonnel.Add(itPersonnel);
            dbContext.SaveChanges();
            return Ok(itPersonnel);


        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateItPersonnel(UpdateItpersonnelDto updateItpersonnelDto)
        {
            object?[]? id = null;
            var itPersonnel = dbContext.itPersonnel.Find(id);
            if (itPersonnel == null)
            {
                return NotFound();
            }
            itPersonnel.Name = updateItpersonnelDto.Name;
            itPersonnel.Email =updateItpersonnelDto.Email;
            itPersonnel.PhoneNumber =updateItpersonnelDto.PhoneNumber;
            itPersonnel.Department = updateItpersonnelDto.Department;
            itPersonnel.Position = updateItpersonnelDto.Position;
            itPersonnel.DateOfBirth = updateItpersonnelDto.DateOfBirth;
            itPersonnel.DateOfJoining = updateItpersonnelDto.DateOfJoining;
            itPersonnel.Address = updateItpersonnelDto.Address;
            dbContext.SaveChanges();
            return Ok(itPersonnel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteItPersonnel(int id)
        {
            var itPersonnel = dbContext.itPersonnel.Find(id);
            if (itPersonnel == null)
            {
                return NotFound();
            }
            dbContext.itPersonnel.Remove(itPersonnel);
            dbContext.SaveChanges();
            return Ok(itPersonnel);
        }
    }
}
