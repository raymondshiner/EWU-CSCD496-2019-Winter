using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        User GetUser(int userId);
        User AddUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int userId);

    }
}