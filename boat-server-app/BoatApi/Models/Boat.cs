using System.ComponentModel.DataAnnotations;
using BoatApi.DTOs.Boat;

namespace BoatApi.Models;

/// <summary>
/// Represents a boat entity in the system, inheriting from <see cref="EntityBase"/>.
/// Contains properties for boat details such as name and description, with validation rules applied.
/// </summary>
internal sealed class Boat(string name, string description) : EntityBase
{
    /// <summary>
    /// Gets or sets the name of the boat. The name must be between 1 and 100 characters.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 100 characters.")]
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the description of the boat. The description must be between 0 and 500 characters.
    /// </summary>
    [StringLength(500, ErrorMessage = "The description cannot be longer than 500 characters.")]
    public string Description { get; set; } = description;

    /// <summary>
    /// Initializes a new instance of the <see cref="Boat"/> class with default values.
    /// Private constructor for entity framework.
    /// </summary>
    private Boat() : this(string.Empty, string.Empty)
    {
    }

    /// <summary>
    /// Updates the boat's name and description with the provided values.
    /// Also updates the LastModified timestamp.
    /// </summary>
    /// <param name="name">The new name of the boat.</param>
    /// <param name="description">The new description of the boat.</param>
    public static Boat Create(string name, string description)
    {
        ValidateInputs(name, description);
        return new Boat(name, description);
    }

    /// <summary>
    /// Updates the boat with the provided update data transfer object (DTO).
    /// Also updates the LastModified timestamp.
    /// </summary>
    /// <param name="dto">The data transfer object containing the new name and description of the boat.</param>
    public void Update(UpdateBoatDto dto) => Update(dto.Name, dto.Description);

    /// <summary>
    /// Updates the boat's name and description with the provided values.
    /// Also updates the LastModified timestamp.
    /// </summary>
    /// <param name="name">The new name of the boat.</param>
    /// <param name="description">The new description of the boat.</param>
    public void Update(string name, string description)
    {
        ValidateInputs(name, description);

        Name = name;
        Description = description;

        UpdateLastModified();
    }

    /// <summary>
    /// Validates the name and description inputs.
    /// Ensures that the name is not null or empty.
    /// </summary>
    /// <param name="name">The name to validate.</param>
    /// <param name="description">The description to validate.</param>
    private static void ValidateInputs(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
    }
}
