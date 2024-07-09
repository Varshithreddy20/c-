using CropDev.Models;

namespace CropDev.Service.Interface
{
    public interface IUserService
    {
        int SignUp(User user);
        (bool, string) Login(string email, string password);
    }
}
