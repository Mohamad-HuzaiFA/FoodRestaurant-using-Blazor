using System.ComponentModel.DataAnnotations;

namespace FoodBistro_Blazor.Models
{
    public class Product
    {
    
            public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(35)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [StringLength(25)]
        public string Category { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [StringLength(100)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Image URL is required")]
        [StringLength(15)]
        public string ImgUrl { get; set; }
     
    }
}
