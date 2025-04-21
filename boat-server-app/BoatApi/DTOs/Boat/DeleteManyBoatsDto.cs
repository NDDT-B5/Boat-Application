namespace BoatApi.DTOs.Boat;

/// <summary>
/// Represents a data transfer object (DTO) for deleting multiple boats.
/// Used to capture the list of boat IDs that need to be deleted from the system.
/// </summary>
/// <param name="Ids">A list of unique identifiers (GUIDs) representing the boats to be deleted.</param>
public record DeleteManyBoatsDto(List<Guid> Ids);