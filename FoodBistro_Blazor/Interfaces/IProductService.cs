using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(Product product);
        Task<List<Product>> GetProducts();
        
        event Action OnChange;

        Task<Product?> GetProductByIdAsync(int id);

        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }


}
