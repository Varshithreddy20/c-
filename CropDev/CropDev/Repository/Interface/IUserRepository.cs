using CropDev.Models;

namespace CropDev.Repository.Interface
{
    public interface IUserRepository
    {
        int SignUp(User user);
        (bool, string) Login(string email, string password);
    }
}
