using BoatApi.Models;

namespace BoatApi.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}