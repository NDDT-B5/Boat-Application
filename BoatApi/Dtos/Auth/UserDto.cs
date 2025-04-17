namespace BoatApi.DTOs.Auth;

public record UserDto(Guid Id, string Username, string Email, string PasswordHash, string Role);