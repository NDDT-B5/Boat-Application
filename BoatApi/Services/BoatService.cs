using AutoMapper;
using BoatApi.Data;
using BoatApi.DTOs.Boat;
using BoatApi.Models;
using BoatApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Services;

/// <inheritdoc />
internal sealed class BoatService(ApplicationDbContext context, IMapper mapper) : IBoatService
{
    /// <inheritdoc />
    public async Task<IEnumerable<BoatDto>> GetAllBoatsAsync()
    {
        var boats = await context.Boats.ToListAsync().ConfigureAwait(false);
        return mapper.Map<List<BoatDto>>(boats);
    }

    /// <inheritdoc />
    public async Task<BoatDto> GetBoatByIdAsync(Guid id)
    {
        var boat = await context.Boats.FindAsync(id).ConfigureAwait(false);
        return boat == null
            ? throw new KeyNotFoundException($"Boat with ID '{id}' was not found.") 
            : mapper.Map<BoatDto>(boat);
    }

    /// <inheritdoc />
    public async Task<BoatDto> CreateBoatAsync(CreateBoatDto dto)
    {
        var boat = mapper.Map<Boat>(dto);

        context.Boats.Add(boat);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return mapper.Map<BoatDto>(boat);
    }

    /// <inheritdoc />
    public async Task UpdateBoatAsync(Guid id, UpdateBoatDto dto)
    {
        var boat = await context.Boats.FindAsync(id).ConfigureAwait(false);

        if (boat == null)
            throw new KeyNotFoundException($"Boat with ID '{id}' was not found.");

        boat.Update(dto);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteBoatAsync(Guid id)
    {
        var boatToDelete = await context.Boats.FindAsync(id).ConfigureAwait(false);
        if (boatToDelete == null)
            throw new KeyNotFoundException($"Boat with ID '{id}' was not found.");

        context.Boats.Remove(boatToDelete);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}