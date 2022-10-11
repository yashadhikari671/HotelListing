using System.ComponentModel.DataAnnotations;

namespace HotelListing.Dtos
{
    public class RegisterDto
    {
        
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
