namespace BoatApi.DTOs.Auth;

/// <summary>
/// Data Transfer Object (DTO) representing the response for authentication,
/// containing a JWT token and the associated user role.
/// </summary>
public record TokenAnswerDto(string JwtToken, string Role);