using BLL.Services;
using DLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University_Api.Model;

namespace University_Api.Controllers
{
    public class StudentController : MainApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Student student)
        {
            return Ok(await _studentService.InsertAsync(student));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetA(string email)
        {
            return Ok(await _studentService.GetAAsync(email));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, Student student)
        {
            return Ok(await _studentService.UpdateAsync(email, student));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            return Ok(await _studentService.DeleteAsync(email));
        }
    }
}
