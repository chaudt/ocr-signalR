using Microsoft.AspNetCore.Mvc;

namespace OCR_SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("RCMS SignalR Service");
    }
}