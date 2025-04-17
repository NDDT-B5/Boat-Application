namespace BoatApi.Services;

using Interfaces;
using AutoMapper;
using Data;
using DTOs.Boat;
using Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<BoatDto?> GetBoatByIdAsync(Guid id)
    {
        var boat = await context.Boats.FindAsync(id).ConfigureAwait(false);
        return boat == null ? null : mapper.Map<BoatDto>(boat);
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
    public async Task<bool> UpdateBoatAsync(Guid id, UpdateBoatDto dto)
    {
        var boat = await context.Boats.FindAsync(id).ConfigureAwait(false);

        if (boat == null)
            return false;

        mapper.Map(dto, boat);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBoatAsync(Guid id)
    {
        var boatToDelete = await context.Boats.FindAsync(id).ConfigureAwait(false);
        if (boatToDelete == null)
            return false;

        context.Boats.Remove(boatToDelete);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}