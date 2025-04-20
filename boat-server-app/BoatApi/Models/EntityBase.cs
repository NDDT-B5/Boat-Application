namespace BoatApi.Models;

/// <summary>
/// Represents the base entity for all models in the application that require an ID, creation date, and last modified date.
/// This class provides common properties for entities in the system and handles automatic tracking of creation and modification timestamps.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Gets the unique identifier for this entity.
    /// The ID is automatically generated when the entity is created.
    /// </summary>
    public Guid Id { get; private init; } = Guid.NewGuid();

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// This value is set to the current UTC time when the entity is first created.
    /// </summary>
    public DateTimeOffset Created { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// This value is updated every time the entity is modified.
    /// </summary>
    public DateTimeOffset LastModified { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Updates the <see cref="LastModified"/> property to the current UTC time.
    /// This method should be called whenever the entity is modified.
    /// </summary>
    public void UpdateLastModified()
	{
		LastModified = DateTimeOffset.UtcNow;
	}
}
