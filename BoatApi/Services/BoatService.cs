namespace BoatApi.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoatApi.Data;
using BoatApi.Models;
using AutoMapper;
using BoatApi.Dtos;

public class BoatService : IBoatService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BoatService> _logger;
    private readonly IMapper _mapper;

    public BoatService(ApplicationDbContext context, ILogger<BoatService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BoatDto>> GetAllBoatsAsync()
    {
        var boats = await _context.Boats.AsNoTracking().ToListAsync();
        return _mapper.Map<List<BoatDto>>(boats);
    }

    public async Task<BoatDto?> GetBoatByIdAsync(Guid id)
    {
        var boat = await _context.Boats.FindAsync(id);

        if (boat == null)
            return null;

        return _mapper.Map<BoatDto>(boat);
    }


    public async Task<BoatDto> CreateBoatAsync(CreateBoatDto dto)
    {
        var boat = _mapper.Map<Boat>(dto);

        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        return _mapper.Map<BoatDto>(boat);
    }

    public async Task<bool> UpdateBoatAsync(Guid id, UpdateBoatDto dto)
    {
        var boat = await _context.Boats.FindAsync(id);

        if (boat == null)
            return false;

        _mapper.Map(dto, boat);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteBoatAsync(Guid id)
    {
        var boatToDelete = await _context.Boats.FindAsync(id);
        if (boatToDelete == null)
            return false;

        _context.Boats.Remove(boatToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}