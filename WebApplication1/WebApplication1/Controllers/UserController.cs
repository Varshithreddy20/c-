using CRAVENEST;
using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Service.Interfce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public UserController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("signup")]
    public IActionResult SignUp([FromBody] User user)
    {
        var result = _userService.SignUp(user);
        if (result == 1)
        {
            return Ok(new { Message = "User registered successfully" });
        }
        return BadRequest(new { Message = "User registration failed" });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        var (success, token, login) = _userService.Login(loginUser.EmailId, loginUser.Password);

        if (success)
        {
            var generatedToken = _jwtService.GenerateToken(token);
            return Ok(new { Token = generatedToken, login.Role, login.Name, login.SignUpId });
        }
        return Unauthorized(new { Message = "Invalid credentials" });
    }

    [HttpPut("update-profile")]
    public IActionResult UpdateProfile([FromBody] UpdateProfileModel updateUser)
    {
        var result = _userService.UpdateSignup(updateUser);

        if (result == 1)
        {
            return Ok(new { Message = "Profile updated successfully" });
        }
        else if (result == 0)
        {
            return NotFound(new { Message = "User not found" });
        }
        return BadRequest(new { Message = "Profile update failed" });
    }


    [HttpPost("confirm-password")]
    public IActionResult ConfirmPassword([FromBody] LoginModel loginUser)
    {
        bool confirmed = _userService.ConfirmPassword(loginUser.EmailId, loginUser.Password);

        if (confirmed)
        {
            return Ok(new { Message = "Password confirmed" });
        }
        return Unauthorized(new { Message = "Password is incorrect" });
    }



}
