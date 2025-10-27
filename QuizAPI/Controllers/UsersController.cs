using Microsoft.AspNetCore.Mvc;
using QuizAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // Step 1: Hardcoded user list (temporary until DB is added)
        private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "1234", Role = "Admin" },
            new User { Id = 2, Username = "user", Password = "abcd", Role = "User" }
        };

        // Step 2: User Registration
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (Users.Any(u => u.Username == user.Username))
                return BadRequest("Username already exists.");

            user.Id = Users.Count + 1;
            Users.Add(user);
            return Ok(user);  // Return the new user info
        }

        // Step 3: User Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = Users.FirstOrDefault(u =>
                u.Username == login.Username && u.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { user.Id, user.Username, user.Role });
        }
    }
}
