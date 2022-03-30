using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace University_Api.Controllers
{
    public class CourseEnrollController : MainApiController
    {
        private readonly ICourseStudentService _courseStudentService;

        public CourseEnrollController(ICourseStudentService courseStudentService)
        {
            _courseStudentService = courseStudentService; 
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CourseAssignInsertViewModel request)
        {
            return Ok(await _courseStudentService.InsertAsyce(request));
        }
    }
}
