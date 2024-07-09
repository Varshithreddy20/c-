using CropDev.Models;
using CropDev.Repository.Interface;
using CropDev.Service.Interface;

namespace CropDev.Service.Concrete
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

        public (bool, string) Login(string email, string password)
        {
            var response = _userRepository.Login(email, password);
            return response;
        }
    }
}
