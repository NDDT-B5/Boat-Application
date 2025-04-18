namespace BoatApi.Infrastructure.Mapping;

using AutoMapper;
using DTOs.Auth;
using Models;

/// <summary>
/// AutoMapper profile for mapping between domain models and DTOs related to users.
/// </summary>
internal sealed class UserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfile"/> class.
    /// Defines mappings between <see cref="User"/> and <see cref="UserDto"/>.
    /// </summary>
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}