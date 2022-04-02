using First.App.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        [HttpGet]
        [Route("GetAllPosts")]
        public IActionResult Get()
        {
            var posts = postService.GetAll();
            return Ok(posts);
        }

       
    }
}

