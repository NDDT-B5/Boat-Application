namespace BoatApi.Infrastructure.Mapping;

using AutoMapper;
using Models;
using DTOs.Boat;

/// <summary>
/// AutoMapper profile for mapping between Boat model and DTOs.
/// This class defines mappings for the <see cref="Boat"/> entity to its respective DTOs.
/// </summary>
internal sealed class BoatProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoatProfile"/> class.
    /// This constructor sets up the mapping configuration between the <see cref="Boat"/> entity 
    /// and its corresponding DTOs (<see cref="BoatDto"/>, <see cref="CreateBoatDto"/>, and <see cref="UpdateBoatDto"/>).
    /// </summary>
    public BoatProfile()
    {
        CreateMap<Boat, BoatDto>();
        CreateMap<CreateBoatDto, Boat>();
        CreateMap<UpdateBoatDto, Boat>();
    }
}