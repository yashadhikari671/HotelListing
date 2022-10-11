using HotelListing.Dtos;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public interface IAuthServices
    {
        Task<bool> ValidateUser(UserDto userDto);
        Task<string> CreateToken();
    }
}
