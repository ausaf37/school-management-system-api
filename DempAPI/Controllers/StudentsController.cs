using DempAPI.Data;
using DempAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace DempAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private  ApplicationDbContext _db;

        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ApplicationDbContext context , ILogger<StudentsController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public List<StudentEntity> GetAllStudents()
        {
            _logger.LogInformation("Fetching All Student List");
            var students = _db.StudentRegister.ToList();
            return students;
        }

        [HttpGet("GetStudentbyId")]
        [Authorize]
        public ActionResult<StudentEntity> GetStudentbyId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var students = _db.StudentRegister.AsTracking().Where( x => x.Id == id).FirstOrDefault();

            if (students == null)
            {
                return NotFound();
            }
            return students;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<StudentEntity> AddStudent([FromBody] StudentEntity studentDetails)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            _db.StudentRegister.Add(studentDetails);
            _db.SaveChanges();
            return Accepted(studentDetails);

        }

        [HttpPost("UpdatestudentDetails")]
        [Authorize]
        public ActionResult<StudentEntity> UpdateStudentDetails(int id , [FromBody] StudentEntity studentDetails)
        {
            if (studentDetails == null)
            {
                return BadRequest(ModelState);
            }

            var students = _db.StudentRegister.FirstOrDefault(x => x.Id == id);

            if (students == null)
            {
                return NotFound();
            }


            students.Name = studentDetails.Name;
            students.Age = studentDetails.Age;
            students.Standard = studentDetails.Standard;
            students.EmailAddress = studentDetails.EmailAddress;
            
            _db.SaveChanges();
            return Accepted(studentDetails);

        }

        [HttpPut("DeleteStudent")]
        [Authorize(Roles ="Admin")]
        public IActionResult DeleteStudentDetails(int id)
        {
            var student = _db.StudentRegister.FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                return NotFound($"Student with Id {id} not found");
            }

            _db.StudentRegister.Remove(student);
            _db.SaveChanges();

            return Ok($"Student deleted successfully. Id = {id}");
        }
    }
}
