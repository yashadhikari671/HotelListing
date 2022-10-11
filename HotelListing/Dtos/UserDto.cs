using System.ComponentModel.DataAnnotations;

namespace HotelListing.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
           
        public string EmailAddress { get; set; }
        [Required(ErrorMessage ="password is required")]
        [DataType(DataType.Password,ErrorMessage ="error with password")]
        public string Password { get; set; }
    }
}
