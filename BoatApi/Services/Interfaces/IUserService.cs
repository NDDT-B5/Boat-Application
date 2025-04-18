using BoatApi.DTOs.Auth;

namespace BoatApi.Services.Interfaces;

/// <summary>
/// Provides user-related services:
/// 1. Verifying user credentials.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Checks if a user exists and if the provided password is valid.
    /// </summary>
    /// <param name="loginDto">The login credentials (username/email and password).</param>
    /// <returns>A <see cref="UserDto"/> if the user exists and the password is valid; otherwise, <c>null</c>.</returns>
    Task<UserDto?> CheckIsValidUserAndPassword(LoginDto loginDto);
}