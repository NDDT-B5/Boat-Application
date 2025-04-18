using System.ComponentModel.DataAnnotations;

namespace BoatApi.Models;

/// <summary>
/// Represents a user entity in the system.
/// Inherits from <see cref="EntityBase"/> which contains common fields like ID and timestamps.
/// </summary>
internal sealed class User(string username, string email, string passwordHash, string role) : EntityBase
{
    /// <summary>
    /// Gets the username of the user.
    /// The username must be between 1 and 20 characters in length.
    /// </summary>
    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 20 characters.")]
    public string Username { get; init; } = username;

    /// <summary>
    /// Gets the email address of the user.
    /// The email must be between 1 and 30 characters in length.
    /// </summary>
    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 30 characters.")]
    public string Email { get; init; } = email;

    /// <summary>
    /// Gets the hashed password of the user.
    /// The password hash must be between 1 and 250 characters in length.
    /// This field is set to private to ensure it is only set at the time of object creation.
    /// </summary>
    [Required]
    [StringLength(250, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 200 characters.")]
    public string PasswordHash { get; private set; } = passwordHash;

    /// <summary>
    /// Gets the role of the user (e.g., Admin, User).
    /// The role must be between 1 and 10 characters in length.
    /// </summary>
    [Required]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 10 characters.")]
    public string Role { get; private set; } = role;

    /// <summary>
    /// Private constructor for EF Core to initialize an empty user.
    /// </summary>
    private User() : this(string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }
}

