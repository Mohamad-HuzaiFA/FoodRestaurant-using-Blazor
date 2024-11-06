using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Services
{
    public interface IUserService
    {
        bool RegisterUser(User newUser);
        bool AuthenticateUser(string email, string password);
        User GetUserById(Guid userId);
        User GetUserByEmail(string email);
    }
}
