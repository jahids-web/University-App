using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace University_Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MainApiController : ControllerBase
    {
    }
}
