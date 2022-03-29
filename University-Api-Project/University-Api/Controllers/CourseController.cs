using BLL.Request;
using BLL.Services;
using DLL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace University_Api.Controllers
{
    public class CourseController : MainApiController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CourseInsertRequestViewMolel request)
        {
            return Ok(await _courseService.InsertAsync(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetA(string code)
        {
            return Ok(await _courseService.GetAAsync(code));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Course aCourse)
        {
            return Ok(await _courseService.UpdateAsync(code, aCourse));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult>Delete(string code)
        {
            return Ok(await _courseService.DeleteAsync(code));
        }


    }
}
