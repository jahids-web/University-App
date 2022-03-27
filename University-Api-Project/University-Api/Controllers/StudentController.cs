using DLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University_Api.Model;

namespace University_Api.Controllers
{
    public class StudentController : MainApiController
    {
        private readonly IStudentRepository _studentReposatory;

        public StudentController(IStudentRepository studentReposatory)
        {
            _studentReposatory = studentReposatory;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Student student)
        {
            return Ok(await _studentReposatory.InsertAsync(student));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentReposatory.GetAllAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetA(string email)
        {
            return Ok(await _studentReposatory.GatAAsync(email));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, Student student)
        {
            return Ok(await _studentReposatory.UpdateAsync(email, student));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            return Ok(await _studentReposatory.DeleteAsync(email));
        }
    }
}
