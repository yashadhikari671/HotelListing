using AutoMapper;
using HotelListing.Data;
using HotelListing.Dtos;

namespace HotelListing.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Register, RegisterDto>().ReverseMap();
            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}
