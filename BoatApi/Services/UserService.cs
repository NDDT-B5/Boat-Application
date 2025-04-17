using AutoMapper;
using BoatApi.Data;
using BoatApi.DTOs.Auth;
using BoatApi.DTOs.Boat;
using BoatApi.Models;
using BoatApi.Seeder;
using BoatApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Services;

public class UserService(ApplicationDbContext context, IMapper mapper) : IUserService
{
    public async Task<UserDto?> CheckIsValidUserAndPassword(LoginDto loginDto)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Username == loginDto.Username || u.Email == loginDto.Username);

        if (user == null)
            return null;

        var hashedPassword = UserSeeder.HashPassword(loginDto.Password);

        return user.PasswordHash != hashedPassword ? null : mapper.Map<UserDto>(user);
    }
}