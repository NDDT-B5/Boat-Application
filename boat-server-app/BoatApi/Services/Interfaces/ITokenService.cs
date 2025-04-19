using BoatApi.DTOs.Auth;

namespace BoatApi.Services.Interfaces;

/// <summary>
/// Service responsible for generating JWT tokens.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="userDto">The user data transfer object containing user information.</param>
    /// <returns>A signed JWT token string.</returns>
    string GenerateToken(UserDto userDto);
}