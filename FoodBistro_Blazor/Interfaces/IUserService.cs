using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(User newUser);
        Task<bool> AuthenticateUserAsync(string email, string password);
        bool VerifyPassword(string enteredPassword, string storedHash);
        string HashPassword(string password);

        //User GetUserById(Guid userId);
        //User GetUserByEmail(string email);
    }
}
