namespace BoatApi.Models;

using System.ComponentModel.DataAnnotations;

public class Boat : EntityBase
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    private Boat()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    private Boat(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Boat Create(string name, string description)
    {
        ValidateInputs(name, description);
        return new Boat(name, description);
    }

    public void Update(string name, string description)
    {
        ValidateInputs(name, description);

        Name = name;
        Description = description;

        UpdateLastModified();
    }

    private static void ValidateInputs(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Title cannot be null or empty.", nameof(name));
    }
}
