using BoatApi.DTOs.Boat;

namespace BoatApi.Services.Interfaces;

/// <summary>
/// Interface for managing boat-related operations.
/// </summary>
public interface IBoatService
{
    /// <summary>
    /// Asynchronously creates a new boat using the provided <see cref="CreateBoatDto"/>.
    /// </summary>
    /// <param name="command">The <see cref="CreateBoatDto"/> containing the boat details.</param>
    /// <returns>A task representing the asynchronous operation. The result is the <see cref="BoatDto"/> of the newly created boat.</returns>
    Task<BoatDto> CreateBoatAsync(CreateBoatDto command);

    /// <summary>
    /// Asynchronously retrieves a boat by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the boat to retrieve.</param>
    /// <returns>A task representing the asynchronous operation. The result is the <see cref="BoatDto"/> if found, or <c>null</c> if no boat is found with the specified <paramref name="id"/>.</returns>
    Task<BoatDto> GetBoatByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously retrieves all boats.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The result is a collection of <see cref="BoatDto"/> representing all boats.</returns>
    Task<IEnumerable<BoatDto>> GetAllBoatsAsync();

    /// <summary>
    /// Asynchronously updates an existing boat with the provided <see cref="UpdateBoatDto"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the boat to update.</param>
    /// <param name="command">The <see cref="UpdateBoatDto"/> containing the updated boat details.</param>
    /// <returns>A task representing the asynchronous operation. The result is <c>true</c> if the boat was successfully updated, otherwise <c>false</c>.</returns>
    Task UpdateBoatAsync(Guid id, UpdateBoatDto command);

    /// <summary>
    /// Asynchronously deletes a boat by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the boat to delete.</param>
    /// <returns>A task representing the asynchronous operation. The result is <c>true</c> if the boat was successfully deleted, otherwise <c>false</c>.</returns>
    Task DeleteBoatAsync(Guid id);
}