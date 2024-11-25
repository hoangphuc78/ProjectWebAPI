using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private List<User> users = new List<User>(); 

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = users.Find(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
            if (user == null)
            {
                return Unauthorized();
            }
          
            return Ok("Token");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            
            return Ok();
        }
    }
}
