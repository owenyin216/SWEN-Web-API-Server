using Microsoft.AspNetCore.Mvc;
using Semester_Projekt.Models;
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

        [HttpPost]
        public IActionResult CreatePost([FromBody] Post newPost)
        {
            try
            {
                _postService.CreatePost(newPost);
                return Ok(new { Message = "Post erfolgreich geuploaded." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPostByID(int id)
        {
            var post = _postService.GetPostByID(id);
            if (post == null)
            {
                return NotFound(new { Message = "Post nicht gefunden." });
            }
            return Ok(post);
        }
        
        [HttpGet]
        public IActionResult GetAllPost()
        {
            var posts = _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _postService.DeletePostByID(id);

                return Ok(new { Message = "Post gelöscht." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] Post updatedPost)
        {
            try
            {
                _postService.UpdatePost(updatedPost);

                return Ok(new { Message = "Post aktualisiert." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        
    }
}
