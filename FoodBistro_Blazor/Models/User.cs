using System.ComponentModel.DataAnnotations;

namespace FoodBistro_Blazor.Models
{
    public class User
    {
        public string Id { get; set; }  
        public string FirstName { get; set; }  
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }  
       
    }
}
