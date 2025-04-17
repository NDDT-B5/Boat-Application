namespace BoatApi.DTOs.Boat;

/// <summary>
/// Represents a data transfer object (DTO) for creating a new boat.
/// Used to capture the necessary data for creating a boat in the system.
/// </summary>
/// <param name="Name">The name of the boat.</param>
/// <param name="Description">The optional description of the boat.</param>
public record CreateBoatDto(string Name, string? Description);