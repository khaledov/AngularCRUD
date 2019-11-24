using Microsoft.AspNetCore.Mvc;

namespace TaskMgr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Task Manager Service Loaded");
    }
}
