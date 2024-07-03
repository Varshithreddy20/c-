using CropDev.Models.Signup;
using CropDev.Utilities.Enums;
namespace CropDev.Service.Interface.SignUp
{
    public interface ISignUpService
    {
        Task<ResultStatus> Create(CreateSignUp createSignUp);
        Task<ResultStatus> ValidateLogin(string emailId, string password);
    }
}