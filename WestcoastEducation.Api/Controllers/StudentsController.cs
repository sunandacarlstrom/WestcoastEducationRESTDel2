using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestcoastEducation.Api.Data;

namespace WestcoastEducationRESTDel1.api.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentsController : ControllerBase
    {
        private readonly WestcoastEducationContext _context;
        public StudentsController(WestcoastEducationContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult> List()
        {
            var result = await _context.Students.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(new { message = $"GetById fungerar {id}" });
        }

        [HttpGet("socialsecurityno/{socialSecurityNo}")]
        public ActionResult GetBySocialSecurityNumber(string socialSecurityNo)
        {
            return Ok(new { message = $"GetBySocialSecurityNumber fungerar {socialSecurityNo}" });
        }

        [HttpGet("email/{email}")]
        public ActionResult GetByEmail(string email)
        {
            return Ok(new { message = $"GetByEmail fungerar {email}" });
        }

        [HttpPost()]
        public ActionResult AddStudent()
        {
            // Gå till databasen och lägg till en ny student...
            return Created(nameof(GetById), new { message = "AddStudent fungerar" });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCourse(int id)
        {
            // Gå till databasen och uppdatera en student...
            return NoContent();
        }

        [HttpGet("list-approved-courses/{id}")]
        public ActionResult ListApprovedCourses(int id)
        {
            // Gå till databasen och lista kurser som en student är anmäld på...
            return Ok(new { message = $"Lista kurser som en student är anmäld på fungerar {id}" });
        }

        [HttpPatch("add-student-to-course/{id}")]
        public ActionResult AddStudentToCourse(int id)
        {
            // Gå till databasen och anmäl en student till nya kurser...
            return NoContent();
        }
    }
}