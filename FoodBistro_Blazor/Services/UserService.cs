using FoodBistro_Blazor.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FoodBistro_Blazor.Interfaces;

namespace FoodBistro_Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> RegisterUserAsync(User newUser)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var checkUser = await db.QueryFirstOrDefaultAsync<User>(
                        "SELECT * FROM Users WHERE Email = @Email", new { newUser.Email });

                    if (checkUser != null)
                    {
                        return false; // User already exists
                    }

                    newUser.Password = HashPassword(newUser.Password);

                    string insertQuery = "INSERT INTO Users (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, @Password)";
                    var result = await db.ExecuteAsync(insertQuery, newUser);

                    return result > 0;
                }
            }
            catch (Exception)
            {
                // Log error
                return false;
            }
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var user = await db.QueryFirstOrDefaultAsync<User>(
                        "SELECT * FROM Users WHERE Email = @Email", new { Email = email });

                    if (user != null && VerifyPassword(password, user.Password))
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                // Log error
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword);
            return hashOfEnteredPassword == storedHash;
        }

        bool IUserService.VerifyPassword(string enteredPassword, string storedHash)
        {
            throw new NotImplementedException();
        }

        string IUserService.HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
