using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectAPI_Selise.Models;
using ProjectAPI_Selise.Repository;

namespace ProjectAPI_Selise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Test successful");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationModel userRegistrationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Couldn't sign up");

            var result = await _userRepository.UserExists(userRegistrationModel.Email);

            if (result == true)
                return BadRequest("User already exists.");

            var reg = await _userRepository.register(userRegistrationModel);
            if (reg == true)
                return Ok("Signup successful");

            return BadRequest("Couldn't sign up.");
        }
    }
}
