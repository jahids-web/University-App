using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace University_Api.Controllers
{
    public class DepartmentController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Hello");
        }
    }
}
