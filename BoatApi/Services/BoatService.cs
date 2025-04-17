using AutoMapper;
using BoatApi.Data;
using BoatApi.Dtos;
using BoatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Services;

/// <inheritdoc />
internal class BoatService : IBoatService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<BoatService> _logger;

    public BoatService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoatDto>> GetAllBoatsAsync()
    {
        var boats = await _context.Boats.ToListAsync().ConfigureAwait(false);
        return _mapper.Map<List<BoatDto>>(boats);
    }

    /// <inheritdoc />
    public async Task<BoatDto?> GetBoatByIdAsync(Guid id)
    {
        var boat = await _context.Boats.FindAsync(id).ConfigureAwait(false);
        return boat == null ? null : _mapper.Map<BoatDto>(boat);
    }

    /// <inheritdoc />
    public async Task<BoatDto> CreateBoatAsync(CreateBoatDto dto)
    {
        var boat = _mapper.Map<Boat>(dto);

        _context.Boats.Add(boat);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return _mapper.Map<BoatDto>(boat);
    }

    /// <inheritdoc />
    public async Task<bool> UpdateBoatAsync(Guid id, UpdateBoatDto dto)
    {
        var boat = await _context.Boats.FindAsync(id).ConfigureAwait(false);

        if (boat == null)
            return false;

        _mapper.Map(dto, boat);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBoatAsync(Guid id)
    {
        var boatToDelete = await _context.Boats.FindAsync(id).ConfigureAwait(false);
        if (boatToDelete == null)
            return false;

        _context.Boats.Remove(boatToDelete);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}