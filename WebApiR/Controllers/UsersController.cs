using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiR.Services;
using WebApiR.Helpers;
using WebApiR.Models;

namespace WebApiR.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        readonly IUserRepository _userRepository;
        private IUserService _userService;

        public UsersController(IUserRepository userRepository, IUserService userService)
        {
            this._userRepository = userRepository;
            this._userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]WebApiR.Entities.User userParam)
        {
            var user = this._userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User _user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (this._userRepository.UserExists(_user.Email, _user.Username) == null)
            {
                if (_user.Password.Length < 6)
                {
                    return BadRequest(new List<String> { "Unable to save in database. Password should have or more characters" });
                }
                var user = this._userRepository.Add(_user);
                if (user.UserId > 0)
                    return Created("/", user);
                else
                    return BadRequest(new List<String> { "Unable to save in database" });
            }
            else
                return BadRequest(new List<String> { "Email/Username Already Exists" });

        }

        [HttpPost("login")]
        public IActionResult Post([FromRoute] string method, [FromBody] WebApiR.Entities.User _user)
        {
            User user = this._userRepository.GetUser(_user.Username, _user.Password);
            user.Password = null;
            if (user == null)
                return BadRequest(new List<String> {"Incorrect Username/Password"});
            return Ok(user);
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<User> users = this._userRepository.Get();
            return Ok(users);
        }
    }
}