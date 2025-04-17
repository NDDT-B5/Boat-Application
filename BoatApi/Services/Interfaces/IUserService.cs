using BoatApi.DTOs.Auth;
using BoatApi.Models;

namespace BoatApi.Services.Interfaces;

public interface IUserService
{
    Task<UserDto?> CheckIsValidUserAndPassword(LoginDto user);
}