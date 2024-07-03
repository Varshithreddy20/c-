using CropDev.Service.Interface.SignUp;
using CropDev.Repository.Interface.SignUp;
using CropDev.Models.Signup;
using CropDev.Utilities.Enums;
using System.Threading.Tasks;

namespace CropDev.Service.Concrete.SignUp
{
    public class SignUpService(ISignUpRepository signUpRepository) : ISignUpService
    {
        public async Task<ResultStatus> Create(CreateSignUp createSignUp)
        {
            return await signUpRepository.Create(createSignUp);
        }

        public async Task<ResultStatus> ValidateLogin(string emailId, string password)
        {
            return await signUpRepository.ValidateLogin(emailId, password);
        }
    }
}
