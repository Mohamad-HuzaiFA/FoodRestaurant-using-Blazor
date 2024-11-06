using FoodBistro_Blazor.Models;

namespace FoodBistro_Blazor.Services
{
    public class CartService : ICartService
    {
        public IList<Product> Cart { get; private set; }
        public int Total { get; set; }

        public event Action OnChange;

        public CartService()
        {
            Cart = new List<Product>();
            Total = 0;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddProduct(Product product)
        {
            if (product != null)
            {
                Cart.Add(product);
                Total += product.Price;
                NotifyStateChanged();
            }
            
        }

        public void DeleteProduct(Product product)
        {
            if (product != null && Cart.Contains(product))
            {
                Cart.Remove(product);
                Total -= product.Price;
                NotifyStateChanged();
            }
        }
    }
}
