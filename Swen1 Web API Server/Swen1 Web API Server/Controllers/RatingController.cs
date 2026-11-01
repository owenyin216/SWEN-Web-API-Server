using Microsoft.AspNetCore.Mvc;
using Semester_Projekt.Models;
using Swen1_Web_API_Server.Service;

namespace Swen1_Web_API_Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;
        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public IActionResult CreateRating([FromBody] Ratings newRating) 
        { 
        
            try
            {
                _ratingService.CreateRating(newRating);
                return Ok(new { Message = "Rating erfolgreich geuploaded." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [HttpGet("post/{postid}")]
        public IActionResult GetRatingByPostID(int postid)
        {
            var ratingsuser = _ratingService.GetAllRatingsFromPostID(postid);
            return Ok(ratingsuser);
        }

        [HttpGet("user/{userid}")]
        public IActionResult GetRatingByUserID(int userid)
        {
            var ratingsuser = _ratingService.GetAllRatingsFromUserID(userid);
            return Ok(ratingsuser);
        }

        [HttpPut]
        public IActionResult UpdateRating([FromBody] Ratings updatedRatings)
        {
            try
            {
                _ratingService.UpdateRating(updatedRatings);

                return Ok(new { Message = "Post aktualisiert." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            try
            {
                _ratingService.DeleteRating(id);

                return Ok(new { Message = "Post gelöscht." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
