using CropDev.Models.Signup;
using CropDev.Utilities.Enums;

namespace CropDev.Repository.Interface.SignUp
{
    public interface ISignUpRepository
    {
        Task<ResultStatus> Create(CreateSignUp createSignUp);
        Task<ResultStatus> ValidateLogin(string emailId, string password);
    }
}
