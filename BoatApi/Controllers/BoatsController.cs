namespace BoatApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Data;
using AutoMapper;
using Dtos;
using Services;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class BoatsController(IMapper mapper, IBoatService boatService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoatDto>>> GetBoatsAsync()
    {
        var boats = await boatService.GetAllBoatsAsync().ConfigureAwait(false);
        return Ok(boats);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BoatDto>> GetBoat(Guid id)
    {
        var boat = await boatService.GetBoatByIdAsync(id).ConfigureAwait(false);
        return boat == null ? NotFound() : Ok(boat);
    }

    [HttpPost]
    public async Task<ActionResult<BoatDto>> CreateBoat(CreateBoatDto dto)
    {
        var boat = await boatService.CreateBoatAsync(dto).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetBoat), new { id = boat.Id }, mapper.Map<BoatDto>(boat));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBoat(Guid id, UpdateBoatDto dto)
    {
        var updated = await boatService.UpdateBoatAsync(id, dto).ConfigureAwait(false);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBoat(Guid id)
    {
        var deleted = await boatService.DeleteBoatAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}