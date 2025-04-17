using System.ComponentModel.DataAnnotations;

namespace BoatApi.Models;
public sealed class User : EntityBase
{
    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 20 characters.")]
    public string Username { get; init; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 30 characters.")]
    public string Email { get; init; }

    [Required]
    [StringLength(250, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 200 characters.")]
    public string PasswordHash { get; private set; }

    [Required]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 10 characters.")]
    public string Role { get; private set; } = "User";

    private User()
    {

    }

    public User(string username, string email, string passwordHash, string role)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}

