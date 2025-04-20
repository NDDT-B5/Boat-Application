using AutoMapper;
using BoatApi.Data;
using BoatApi.DTOs.Boat;
using BoatApi.Models;
using BoatApi.Services;
using BoatApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Tests.Services;

public class BoatServiceTests
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly IBoatService _boatService;

    public BoatServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Boat, BoatDto>();
            cfg.CreateMap<CreateBoatDto, Boat>();
        });
        _mapper = config.CreateMapper();

        _context = CreateDbContext();
        _boatService = new BoatService(_context, _mapper);
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetAllBoatsAsync_ReturnsMappedList()
    {
        // Arrange
        var boats = new List<Boat>
        {
            new("Boat 1", "Desc 1"),
            new("Boat 2", "Desc 2")
        };
        _context.Boats.AddRange(boats);
        await _context.SaveChangesAsync();

        // Act
        var result = await _boatService.GetAllBoatsAsync();

        // Assert
        var boatDtos = result as BoatDto[] ?? result.ToArray();
        Assert.Equal(2, boatDtos.Count());
        Assert.Contains(boatDtos, b => b.Id == boats[0].Id);
        Assert.Contains(boatDtos, b => b.Id == boats[1].Id);
    }

    [Fact]
    public async Task GetBoatByIdAsync_ReturnsMappedBoat_WhenExists()
    {
        // Arrange
        var boat = new Boat("Sailor 1", "Desc 1");
        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        // Act
        var result = await _boatService.GetBoatByIdAsync(boat.Id);

        // Assert
        Assert.Equal(boat.Id, result.Id);
        Assert.Equal(boat.Name, result.Name);
        Assert.Equal(boat.Description, result.Description);
    }

    [Fact]
    public async Task GetBoatByIdAsync_Throws_WhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act, Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _boatService.GetBoatByIdAsync(id));
        Assert.Contains("not found", ex.Message);
    }

    [Fact]
    public async Task CreateBoatAsync_AddsAndReturnsMappedBoat()
    {
        // Arrange
        var dto = new CreateBoatDto("new Boat", "new Desc");

        // Act
        var result = await _boatService.CreateBoatAsync(dto);

        // Assert
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Description, result.Description);
        Assert.Single(_context.Boats);
    }

    [Fact]
    public async Task UpdateBoatAsync_Updates_WhenExists()
    {
        // Arrange
        var boat = new Boat("Old", "Old");
        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        var updateDto = new UpdateBoatDto("Updated", "Updated");

        // Act
        await _boatService.UpdateBoatAsync(boat.Id, updateDto);

        // Assert
        var updated = await _context.Boats.FindAsync(boat.Id);
        Assert.Equal(updateDto.Name, updated?.Name);
        Assert.Equal(updateDto.Description, updated?.Description);
    }

    [Fact]
    public async Task UpdateBoatAsync_Throws_WhenNotFound()
    {
        // Arrange
        var dto = new UpdateBoatDto("New", "New");

        // Act, Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _boatService.UpdateBoatAsync(Guid.NewGuid(), dto));

        // Assert
        Assert.Contains("not found", ex.Message);
    }

    [Fact]
    public async Task DeleteBoatAsync_Removes_WhenExists()
    {
        // Arrange
        var boat = new Boat("Delete Me", "Delete Me");
        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        var service = new BoatService(_context, _mapper);

        // Act
        await service.DeleteBoatAsync(boat.Id);

        // Assert
        Assert.Empty(_context.Boats);
    }

    [Fact]
    public async Task DeleteBoatAsync_Throws_WhenNotFound()
    {
        // Act, Asser
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _boatService.DeleteBoatAsync(Guid.NewGuid()));

        // Assert
        Assert.Contains("not found", ex.Message);
    }
}
