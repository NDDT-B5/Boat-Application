namespace BoatApi.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a boat entity in the system, inheriting from <see cref="EntityBase"/>.
/// Contains properties for boat details such as name and description, with validation rules applied.
/// </summary>
public sealed class Boat : EntityBase
{
    /// <summary>
    /// Gets or sets the name of the boat. The name must be between 1 and 100 characters.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 100 characters.")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the boat. The description must be between 0 and 500 characters.
    /// </summary>
    [StringLength(500, ErrorMessage = "The description cannot be longer than 500 characters.")]
    public string Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Boat"/> class with default values.
    /// Private constructor for entity framework.
    /// </summary>
    private Boat()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Boat"/> class.
    /// Validates the input values and initializes the boat with the provided name and description.
    /// </summary>
    /// <param name="name">The name of the boat.</param>
    /// <param name="description">The description of the boat.</param>
    /// <returns>A new <see cref="Boat"/> instance.</returns>
    private Boat(string name, string description)
    {
        Name = name;
        Description = description;
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
