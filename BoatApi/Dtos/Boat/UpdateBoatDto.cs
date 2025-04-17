namespace BoatApi.DTOs.Boat;

/// <summary>
/// Represents a data transfer object (DTO) for updating an existing boat.
/// Used to capture the necessary data to modify a boat in the system.
/// </summary>
/// <param name="Name">The name of the boat to update.</param>
/// <param name="Description">The optional description of the boat to update.</param>
public record UpdateBoatDto(string Name, string? Description);