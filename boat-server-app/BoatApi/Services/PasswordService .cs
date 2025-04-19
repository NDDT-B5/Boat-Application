using System.Security.Cryptography;
using System.Text;
using BoatApi.Services.Interfaces;

namespace BoatApi.Services;

/// <inheritdoc />
internal sealed class PasswordService : IPasswordService
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }
}