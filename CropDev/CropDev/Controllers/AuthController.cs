using CropDev.JwtInterface;
using CropDev.Models;
using CropDev.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDev.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserRepository userRepository, IJwtService jwtService) : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult SignUp(User user)
        {
            var result = userRepository.SignUp(user);
            if (result == 1)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest(new { Message = "User registration failed" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var (success, token) = userRepository.Login(loginUser.EmailId, loginUser.Password);
            var generatedToken = jwtService.GenerateToken(token);
            if (success)
            {
                return Ok(new { Token = generatedToken });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }
}
