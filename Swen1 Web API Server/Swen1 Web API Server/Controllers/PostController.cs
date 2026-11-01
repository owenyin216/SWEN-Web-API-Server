using Microsoft.AspNetCore.Mvc;
using Swen1_Web_API_Server.Service;

namespace Swen1_Web_API_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController: ControllerBase
    {
        private readonly PostService _postService;
        public PostController(PostService postService)
        {
            _postService = postService;
        }
    }
}
