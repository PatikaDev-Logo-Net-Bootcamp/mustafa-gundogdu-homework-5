using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllPosts")]
        public IActionResult Get()
        {
            
            return Ok("Hello World");
        }

       
    }
}
}
