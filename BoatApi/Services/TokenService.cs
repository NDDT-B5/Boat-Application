using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BoatApi.DTOs.Auth;
using BoatApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BoatApi.Services;

/// <inheritdoc />
internal sealed class TokenService(IConfiguration config) : ITokenService
{
    /// <inheritdoc />
    public string GenerateToken(UserDto userDto)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}