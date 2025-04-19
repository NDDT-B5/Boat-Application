namespace BoatApi.Services.Interfaces;

/// <summary>
/// Provides utility methods for working with passwords.
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hashes the password using SHA256.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password as a string.</returns>
    string HashPassword(string password);
}