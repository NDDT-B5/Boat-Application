namespace BoatApi.DTOs.Auth;

/// <summary>
/// Data Transfer Object (DTO) representing the login credentials of a user.
/// </summary>
public record LoginDto(string Username, string Password);