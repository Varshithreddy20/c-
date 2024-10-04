using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;

namespace CRAVENEST.Service.Interfce
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int SignUp(User user)
        {
            return _userRepository.SignUp(user);
        }

        public (bool, string, Login) Login(string email, string password)
        {
            return _userRepository.Login(email, password);
        }

        public int UpdateSignup(UpdateProfileModel user)
        {
            return _userRepository.UpdateSignup(user);
        }

        public bool ConfirmPassword(string email, string password)
        {
            return _userRepository.ConfirmPassword(email, password);
        }

    }
}
