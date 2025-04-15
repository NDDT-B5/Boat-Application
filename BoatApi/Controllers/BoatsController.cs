namespace BoatApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Data;
using AutoMapper;
using Dtos;
using Services;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class BoatsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IBoatService _boatService;

    public BoatsController(ApplicationDbContext context, IMapper mapper, IBoatService boatService)
    {
        _context = context;
        _mapper = mapper;
        _boatService = boatService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoatDto>>> GetBoatsAsync()
    {
        var boats = await _boatService.GetAllBoatsAsync().ConfigureAwait(false);
        return Ok(boats);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BoatDto>> GetBoat(Guid id)
    {
        var boat = await _boatService.GetBoatByIdAsync(id).ConfigureAwait(false);
        return boat == null ? NotFound() : Ok(boat);
    }

    [HttpPost]
    public async Task<ActionResult<BoatDto>> CreateBoat(CreateBoatDto dto)
    {
        var boat = await _boatService.CreateBoatAsync(dto).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetBoat), new { id = boat.Id }, _mapper.Map<BoatDto>(boat));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBoat(Guid id, UpdateBoatDto dto)
    {
        var updated = await _boatService.UpdateBoatAsync(id, dto).ConfigureAwait(false);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBoat(Guid id)
    {
        var deleted = await _boatService.DeleteBoatAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}