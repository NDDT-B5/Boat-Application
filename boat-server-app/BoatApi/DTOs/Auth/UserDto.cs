namespace BoatApi.DTOs.Auth;

/// <summary>
/// Data Transfer Object (DTO) representing a user in the system.
/// Used for transferring user information between layers of the application.
/// </summary>
public record UserDto(Guid Id, string Username, string Email, string PasswordHash, string Role);