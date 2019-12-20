using Microsoft.AspNetCore.Mvc;
using EventBee.Models;
using Microsoft.AspNetCore.Authorization;
using EventBee.Repositories.Interfaces;
using EventBee.Models.Users;

namespace EventBee.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userRepository.GetAll();
            if (users == null || users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] User user)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }
            var updatedUser = _userRepository.Update(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }
            var user = _userRepository.DeleteById(id);
            if (user == false)
            {
                return BadRequest("Deletion failed");
            }

            return Ok("Object deleted");
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userRepository.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]User userParam)
        {
            if (userParam.FirstName == "" || userParam.FirstName == null)
            {
                return BadRequest("You have to fill your name in registration form");
            }
            if (userParam.Surname == "" || userParam.Surname == null)
            {
                return BadRequest("You have to fill your surname in registration form");
            }
            if (userParam.Username == "" || userParam.Username == null)
            {
                return BadRequest("You have to fill your username in registration form");
            }
            if (userParam.Password == "" || userParam.Password == null)
            {
                return BadRequest("You have to fill your password in registration form");
            }
            var user = _userRepository.Register(userParam);

            if (user == null)
                return BadRequest("You have to choose another username!");

            return Ok(user.Username);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}
