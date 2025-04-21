using BoatApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BoatApi.DTOs.Boat;
using Microsoft.AspNetCore.Authorization;

namespace BoatApi.Controllers;

/// <summary>
/// Controller for managing boats in the system.
/// Handles CRUD operations related to boats.
/// </summary>
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class BoatsController(IMapper mapper, IBoatService boatService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all boats.
    /// </summary>
    /// <returns>A list of <see cref="BoatDto"/> objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BoatDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<BoatDto>>> GetBoatsAsync()
    {
        var boats = await boatService.GetAllBoatsAsync().ConfigureAwait(false);
        return Ok(boats);
    }

    /// <summary>
    /// Retrieves a specific boat by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the boat.</param>
    /// <returns>The matching <see cref="BoatDto"/> or 404 if not found.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BoatDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<BoatDto>> GetBoat(Guid id)
    {
        var boat = await boatService.GetBoatByIdAsync(id).ConfigureAwait(false);
        return Ok(boat);
    }

    /// <summary>
    /// Creates a new boat.
    /// </summary>
    /// <param name="dto">The data transfer object containing information about the boat to create.</param>
    /// <returns>The created <see cref="BoatDto"/> with a 201 status code.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BoatDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<BoatDto>> CreateBoat(CreateBoatDto dto)
    {
        var boat = await boatService.CreateBoatAsync(dto).ConfigureAwait(false);

        return CreatedAtAction(nameof(GetBoat), new { id = boat.Id }, mapper.Map<BoatDto>(boat));
    }

    /// <summary>
    /// Updates an existing boat with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the boat to update.</param>
    /// <param name="dto">The data transfer object containing the updated boat information.</param>
    /// <returns>A response indicating the result of the update operation.</returns>
    /// <response code="204">Boat was successfully updated.</response>
    /// <response code="404">The boat specified in the list was not found.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateBoat(Guid id, UpdateBoatDto dto)
    {
        await boatService.UpdateBoatAsync(id, dto).ConfigureAwait(false);
        return NoContent();
    }

    /// <summary>
    /// Deletes a boat by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the boat to delete.</param>
    /// <returns>A response indicating the result of the deletion operation.</returns>
    /// <response code="204">Boat was successfully deleted.</response>
    /// <response code="404">The boat specified in the list was not found.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteBoat(Guid id)
    {
        await boatService.DeleteBoatAsync(id).ConfigureAwait(false);
        return NoContent();
    }

    /// <summary>
    /// Deletes multiple boats from the system.
    /// This action allows for deleting many boats at once by specifying a list of boat IDs.
    /// </summary>
    /// <param name="dto">The DTO containing the list of boat IDs to be deleted.</param>
    /// <returns>A response indicating the result of the deletion operation.</returns>
    /// <response code="204">Boats were successfully deleted.</response>
    /// <response code="400">The provided list of boat IDs is invalid.</response>
    /// <response code="404">One or more boats specified in the list were not found.</response>
    [HttpDelete("delete-many")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMany(DeleteManyBoatsDto dto)
    {
        await boatService.DeleteManyAsync(dto);
        return NoContent();
    }
}