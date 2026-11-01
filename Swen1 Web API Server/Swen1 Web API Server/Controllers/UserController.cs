using Microsoft.AspNetCore.Mvc;
using Semester_Projekt.Models;
using Swen1_Web_API_Server.Service;

namespace Swen1_Web_API_Server.Controllers
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            try
            {
                _userService.RegisterUser(newUser);
                return Ok(new { Message = "Benutzer erfolgreich registriert." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _userService.LoginUserByUsername(request.Username, request.Password);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var user = _userService.GetUserByID(id);
            if (user == null)
            {
                return NotFound(new { Message = "Benutzer nicht gefunden." });
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUserByID(id);

                return Ok(new { Message = "Benutzer gelöscht." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User updatedUser)
        {
            try
            {
                _userService.UpdateUserProfile(updatedUser);

                return Ok(new { Message = "Profil aktualisiert." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }

}
