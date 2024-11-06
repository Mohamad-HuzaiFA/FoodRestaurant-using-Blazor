using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Services
{
    public class ProductService
    {
        public List<Product> GetProducts()
        {
            // You can return products from a database or static list
            return new List<Product>
            {
                new Product { Id = 1, Name = "Classic Burger", Price = 250, Category = "burger", ImgUrl = "Assets/Products/pro-02.png" },
                new Product { Id = 2, Name = "Cheese Pizza", Price = 500, Category = "pizza", ImgUrl = "Assets/Products/pro-03.png" }
            };
        }
    }
}
