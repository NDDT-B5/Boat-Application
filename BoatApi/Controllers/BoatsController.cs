using BoatApi.Services.Interfaces;

namespace BoatApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Services.Interfaces;
using DTOs.Boat;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<ActionResult<BoatDto>> GetBoat(Guid id)
    {
        var boat = await boatService.GetBoatByIdAsync(id).ConfigureAwait(false);
        return boat == null ? NotFound() : Ok(boat);
    }

    /// <summary>
    /// Creates a new boat.
    /// </summary>
    /// <param name="dto">The data transfer object containing information about the boat to create.</param>
    /// <returns>The created <see cref="BoatDto"/> with a 201 status code.</returns>
    [HttpPost]
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
    /// <returns>A 204 No Content status code upon successful update.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBoat(Guid id, UpdateBoatDto dto)
    {
        var updated = await boatService.UpdateBoatAsync(id, dto).ConfigureAwait(false);
        return NoContent();
    }

    /// <summary>
    /// Deletes a boat by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the boat to delete.</param>
    /// <returns>A 204 No Content status code upon successful deletion.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBoat(Guid id)
    {
        var deleted = await boatService.DeleteBoatAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}