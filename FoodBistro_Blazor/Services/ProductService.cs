using Dapper;
using FoodBistro_Blazor.Interfaces;
using FoodBistro_Blazor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodBistro_Blazor.Services
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString;

        public event Action? OnChange;

        public ProductService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Create
        public async Task<bool> AddProductAsync(Product product)
        {
            const string query = @"
                INSERT INTO Products (Name, Price, Category, ImgUrl)
                VALUES (@Name, @Price, @Category, @ImgUrl);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = CreateConnection();
            var success = await connection.QuerySingleAsync<int>(query, product) > 0;

            if (success)
            {
                NotifyStateChanged();
            }

            return success;
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
                UPDATE Products 
                SET Name = @Name, Price = @Price, Category = @Category, ImgUrl = @ImgUrl
                WHERE Id = @Id;";

            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(query, product);

            if (rowsAffected > 0)
            {
                NotifyStateChanged();
            }

            return rowsAffected > 0;
        }

        // Delete
        public async Task<bool> DeleteProductAsync(int id)
        {
            const string query = "DELETE FROM Products WHERE Id = @Id;";

            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            if (rowsAffected > 0)
            {
                NotifyStateChanged();
            }

            return rowsAffected > 0;
        }
    }
}
