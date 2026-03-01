using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Services;


namespace SchoolManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApiService _apiService;

        public StudentsController()
        {
            _apiService = new ApiService();
        }
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Index()
        {
            List<StudentEntity> lstStudents = new List<StudentEntity>();
            string token = HttpContext.Session.GetString("APIToken");
            lstStudents = await _apiService.GetAllStudents(token);
            return View(lstStudents);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent(StudentEntity StudentEntity)
        {
            string token = HttpContext.Session.GetString("APIToken");
            bool isSuccess = await _apiService.AddStudent(StudentEntity, token);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("ErrorPage");
            }
        }

        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Details(int Id) 
        {
            string token = HttpContext.Session.GetString("APIToken");
            StudentEntity student = new StudentEntity();
            student.Id = Id;
            student = await _apiService.GetStudentbyId(Id, token);
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudentDetails(int id,StudentEntity studentEntity)
        {
            string token = HttpContext.Session.GetString("APIToken");
            bool isSuccess = await _apiService.UpdateStudentDetails(id,studentEntity, token);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("ErrorPage");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            string token = HttpContext.Session.GetString("APIToken");
            bool isSuccess = await _apiService.DeleteStudent(Id, token);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("ErrorPage");
            }

        }

        public IActionResult ErrorPage()
        {
            return View();
        }

    }
}
