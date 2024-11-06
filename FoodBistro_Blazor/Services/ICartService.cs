using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Services
{
    public interface ICartService
    {
        IList<Product> Cart { get; }
        int Total { get; set; }

        event Action OnChange;
        void AddProduct(Product product);
        void DeleteProduct(Product product);
    }
}
