using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Services
{
        public interface IProductService
        {
            Task<int> AddProductAsync(Product product);
            Task<List<Product>> GetProducts();
            Task<Product?> GetProductByIdAsync(int id);

            Task<bool> UpdateProductAsync(Product product);
            Task<bool> DeleteProductAsync(int id);
        }

    
}
