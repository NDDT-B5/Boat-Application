namespace BoatApi.DTOs.Boat;

/// <summary>
/// Represents a data transfer object (DTO) for a boat.
/// Used to transfer boat data between different layers of the application.
/// </summary>
/// <param name="Id">The unique identifier for the boat.</param>
/// <param name="Name">The name of the boat.</param>
/// <param name="Description">The optional description of the boat.</param>
public record BoatDto(Guid Id, string Name, string? Description);