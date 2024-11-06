using FoodBistro_Blazor.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodBistro_Blazor.Services
{
    public class UserService : IUserService
    {
        
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool RegisterUser(User newUser)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Check if user exists alraedy
                var checkUser = db.QueryFirstOrDefault<User>("select * from USERS where Email = @Email", new { newUser.Email });
                if (checkUser != null)
                {
                    return false;
                }

                string insertQuery = @"insert into USERS values (@FirstName, @LastName, @Email, @Password)";
                var result = db.Execute(insertQuery, newUser);
                return result > 0;
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var user = db.QueryFirstOrDefault<User>(
                    "SELECT * FROM Users WHERE Email = @Email AND Password = @Password",
                    new { Email = email, Password = password });
                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }


        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

    }
}
