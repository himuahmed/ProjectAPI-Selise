using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI_Selise.Models;
using ProjectAPI_Selise.Repository;

namespace ProjectAPI_Selise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, UserManager<UserModel> userManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _configuration = configuration;
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

        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Login failed");

            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
            if (user == null)
                return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);

            if (!result)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AuthSetting:tokenKey").Value));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}
