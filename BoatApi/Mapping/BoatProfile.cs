namespace BoatApi.Mapping;

using AutoMapper;
using BoatApi.Models;
using BoatApi.Dtos;

public class BoatProfile : Profile
{
    public BoatProfile()
    {
        CreateMap<Boat, BoatDto>();
        CreateMap<CreateBoatDto, Boat>();
        CreateMap<UpdateBoatDto, Boat>();
    }
}
