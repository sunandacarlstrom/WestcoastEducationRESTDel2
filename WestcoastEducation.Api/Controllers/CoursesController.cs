using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestcoastEducation.Api.Data;
using WestcoastEducation.Api.ViewModels;

namespace WestcoastEducationRESTDel1.api.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly WestcoastEducationContext _context;
        public CoursesController(WestcoastEducationContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult> List()
        {
            var result = await _context.Courses
            // talar om för EF Core att när du listar Courses vill jag också att du inkluderar det som finns i Teacher-tabellen där jag har en INNER JOIN masking, 
            // alltså teacherId med ett visst värde i Courses måste existera i Teacher som id-kolumn.
            .Include(t => t.Teacher)
            // projicerar resultatet in i min ViewListModel 
            .Select(c => new CourseListViewModel
            {
                //Här definierar jag vilka kolumner som jag egentligen vill ha tillbaka i (SQL)frågan 
                Id = c.Id,
                // använder en coalesce operation
                Teacher = c.Teacher.Name ?? "",
                Number = c.Number,
                Name = c.Name,
                Title = c.Title
            })
            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _context.Courses
            .Include(t => t.Teacher)
            // måste ha en vymodell (DTO) som vi kan flytta över data ifrån den här frågan till en modell som json kan retunera 
            // projicerar resultatet 
            .Select(c => new CourseDetailsViewModel
            {
                //Här definierar jag vilka kolumner som jag egentligen vill ha tillbaka i (SQL)frågan 
                Id = c.Id,
                // använder en coalesce operation
                Teacher = c.Teacher.Name ?? "",
                Number = c.Number,
                Name = c.Name,
                Title = c.Title,
                Start = c.Start,
                End = c.End,
                Content = c.Content
            })
            // jag vill ha tag i ett Id som stämmer överrens med det Id som jag skickar in 
            .SingleOrDefaultAsync(c => c.Id == id);
            return Ok(result);
        }

        [HttpGet("courseno/{courseNo}")]
        public async Task<ActionResult> GetByCourseNumber(string courseNo)
        {
            var result = await _context.Courses
            .Include(t => t.Teacher)
            .Select(c => new CourseDetailsViewModel
            {
                Id = c.Id,
                Teacher = c.Teacher.Name ?? "",
                Number = c.Number,
                Name = c.Name,
                Title = c.Title,
                Start = c.Start,
                End = c.End,
                Content = c.Content
            })
            .SingleOrDefaultAsync(c => c.Number!.ToUpper().Trim() == courseNo.ToUpper().Trim());
            return Ok(result);
        }

        [HttpGet("coursetitle/{courseTitle}")]
        public async Task<ActionResult> GetByCourseTitle(string courseTitle)
        {
            var result = await _context.Courses
            .Include(t => t.Teacher)
            .Where(s => s.Title!.ToUpper().Trim() == courseTitle.ToUpper().Trim())
            .Select(c => new CourseDetailsViewModel
            {
                Id = c.Id,
                Teacher = c.Teacher.Name ?? "",
                Number = c.Number,
                Name = c.Name,
                Title = c.Title,
                Start = c.Start,
                End = c.End,
                Content = c.Content
            })
            // listar alla kurser som WU22 programmets elever har under sin studietid
            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("teacher/{teacher}")]
        public async Task<ActionResult> GetByTeacher(string teacher)
        {
            var result = await _context.Courses
            .Include(t => t.Teacher)
            // sätter ett villkor där jag beskriver läraren som håller i x kurser
            .Where(s => s.Teacher.Name!.ToUpper().Trim() == teacher.ToUpper().Trim())
            .Select(c => new CourseDetailsViewModel
            {
                Id = c.Id,
                Teacher = c.Teacher.Name ?? "",
                Number = c.Number,
                Name = c.Name,
                Title = c.Title,
                Start = c.Start,
                End = c.End,
                Content = c.Content
            })
            // listar alla kurser som en specifik lärare håller i 
            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("coursestart/{courseStart}")]
        public async Task<ActionResult> GetByCourseStart(string courseStart)
        {
            var result = await _context.Courses
            .Include(t => t.Teacher)
            .Select(c => new CourseListViewModel
            {
                Id = c.Id,
                Teacher = c.Teacher.Name ?? "",
                Number = c.Number,
                Name = c.Name,
                Title = c.Title
            })
            // listar alla kurser som startar samtidigt
            .ToListAsync();
            return Ok(result);
        }

        [HttpPost()]
        public ActionResult AddCourse()
        {
            // Gå till databasen och lägg till en ny kurs...
            return Created(nameof(GetById), new { message = "AddCourse fungerar" });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCourse(int id)
        {
            // Gå till databasen och uppdatera en ny kurs...
            return NoContent();
        }

        [HttpPatch("markasfull/{id}")]
        public ActionResult MarkAsFull(int id)
        {
            // Gå till databasen och markera en kurs som fullbokad...
            return NoContent();
        }

        [HttpPatch("markasdone/{id}")]
        public ActionResult MarkAsDone(int id)
        {
            // Gå till databasen och markera en kurs som avklarad...
            return NoContent();
        }
    }
}