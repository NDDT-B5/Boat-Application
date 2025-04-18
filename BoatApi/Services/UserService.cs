using AutoMapper;
using BoatApi.Data;
using BoatApi.DTOs.Auth;
using BoatApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Services;

/// <inheritdoc />
internal sealed class UserService(ApplicationDbContext context, IMapper mapper, IPasswordService passwordService) : IUserService
{
    /// <inheritdoc />
    public async Task<UserDto?> CheckIsValidUserAndPassword(LoginDto loginDto)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Username == loginDto.Username || u.Email == loginDto.Username);

        if (user == null)
            return null;

        var hashedPassword = passwordService.HashPassword(loginDto.Password);

        return user.PasswordHash != hashedPassword ? null : mapper.Map<UserDto>(user);
    }
}