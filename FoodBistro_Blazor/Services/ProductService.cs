using Dapper;
using FoodBistro_Blazor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodBistro_Blazor.Services
{
    public class ProductService : IProductService
    {
        
            private readonly string _connectionString;

            public ProductService(string connectionString)
            {
                _connectionString = connectionString;
            }

            private IDbConnection CreateConnection()
            {
                return new SqlConnection(_connectionString);
            }

            // Create
            public async Task<int> AddProductAsync(Product product)
            {
                const string query = @"
            INSERT INTO Products (Name, Price, Category, ImgUrl)
            VALUES (@Name, @Price, @Category, @ImgUrl);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<int>(query, product);
            }

        // Read All
        public async Task<List<Product>> GetProducts()
        {
            const string query = "SELECT * FROM Products;";

            using var connection = CreateConnection();
            var products = await connection.QueryAsync<Product>(query);
            return products.ToList(); // Convert IEnumerable<Product> to List<Product>
        }

        // Read By ID
        public async Task<Product?> GetProductByIdAsync(int id)
            {
                const string query = "SELECT * FROM Products WHERE Id = @Id;";

                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
            }

            // Update
            public async Task<bool> UpdateProductAsync(Product product)
            {
                const string query = @"
            UPDATE Products SET Name = @Name,Price = @Price, Category = @Category, ImgUrl = @ImgUrl
            WHERE Id = @Id;";

                using var connection = CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(query, product);
                return rowsAffected > 0;
            }

            // Delete
            public async Task<bool> DeleteProductAsync(int id)
            {
                const string query = "DELETE FROM Products WHERE Id = @Id;";

                using var connection = CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
    }

    

}




//public List<Product> GetProducts()
//{
//    // You can return products from a database or static list
//    return new List<Product>
//    {
//        new Product { Id = 1, Name = "Classic Burger", Price = 250, Category = "burger", ImgUrl = "Assets/Products/pro-02.png" },
//        new Product { Id = 2, Name = "Cheese Pizza", Price = 500, Category = "pizza", ImgUrl = "Assets/Products/pro-03.png" }
//    };
//}