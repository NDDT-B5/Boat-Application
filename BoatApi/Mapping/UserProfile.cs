using AutoMapper;
using BoatApi.DTOs.Auth;
using BoatApi.DTOs.Boat;
using BoatApi.Models;

namespace BoatApi.Mapping
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
