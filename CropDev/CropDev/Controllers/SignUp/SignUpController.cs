using CropDev.Models.Signup;
using CropDev.Service.Interface.SignUp;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CropDev.Models.Login;

namespace CropDev.Controllers.SignUp
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ISignUpService _signUpService;

        public LoginController(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Create([FromBody] CreateSignUp createSignUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _signUpService.Create(createSignUp);
            return result switch
            {
                ResultStatus.Success => Ok("User created successfully."),
                ResultStatus.DuplicateEntry => Conflict("User already exists."),
                ResultStatus.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request."),
                _ => BadRequest("Unable to create the user."),
            };
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _signUpService.ValidateLogin(loginModel.EmailId, loginModel.Password);
            return result switch
            {
                ResultStatus.Success => Ok("Login successful!"),
                ResultStatus.Failed => Unauthorized("Invalid email or password."),
                ResultStatus.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request."),
                _ => BadRequest("Unable to process login request."),
            };
        }
    }
}
