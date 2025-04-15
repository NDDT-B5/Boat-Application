namespace BoatApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoatApi.Data;
using BoatApi.Models;
using AutoMapper;
using BoatApi.Dtos;

[Route("api/[controller]")]
[ApiController]
public class BoatsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public BoatsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoatDto>>> GetBoats()
    {
        var boats = await _context.Boats.ToListAsync();
        return Ok(_mapper.Map<List<BoatDto>>(boats));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoatDto>> GetBoat(Guid id)
    {
        var boat = await _context.Boats.FindAsync(id);
        return boat == null ? NotFound() : Ok(_mapper.Map<BoatDto>(boat));
    }

    [HttpPost]
    public async Task<ActionResult<BoatDto>> CreateBoat(CreateBoatDto dto)
    {
        var boat = _mapper.Map<Boat>(dto);

        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBoat), new { id = boat.Id }, _mapper.Map<BoatDto>(boat));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBoat(Guid id, UpdateBoatDto dto)
    {
        var boat = await _context.Boats.FindAsync(id);
        if (boat == null) return NotFound();

        _mapper.Map(dto, boat);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBoat(Guid id)
    {
        var boat = await _context.Boats.FindAsync(id);
        if (boat == null) return NotFound();

        _context.Boats.Remove(boat);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}