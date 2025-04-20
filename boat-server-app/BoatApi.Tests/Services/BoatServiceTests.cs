using AutoMapper;
using BoatApi.Data;
using BoatApi.DTOs.Boat;
using BoatApi.Models;
using BoatApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Tests.Services;

public class BoatServiceTests
{
    private readonly IMapper _mapper;

    public BoatServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Boat, BoatDto>();
            cfg.CreateMap<CreateBoatDto, Boat>();
        });
        _mapper = config.CreateMapper();
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
        var context = CreateDbContext();
        var boats = new List<Boat>
        {
            new("Boat 1", "Desc 1"),
            new("Boat 2", "Desc 2")
        };
        context.Boats.AddRange(boats);
        await context.SaveChangesAsync();

        var service = new BoatService(context, _mapper);

        // Act
        var result = await service.GetAllBoatsAsync();

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
        var context = CreateDbContext();
        var boat = new Boat("Sailor 1", "Desc 1");
        context.Boats.Add(boat);
        await context.SaveChangesAsync();

        var service = new BoatService(context, _mapper);

        // Act
        var result = await service.GetBoatByIdAsync(boat.Id);

        // Assert
        Assert.Equal(boat.Id, result.Id);
        Assert.Equal(boat.Name, result.Name);
        Assert.Equal(boat.Description, result.Description);
    }

    [Fact]
    public async Task GetBoatByIdAsync_Throws_WhenNotFound()
    {
        // Arrange
        var context = CreateDbContext();
        var service = new BoatService(context, _mapper);

        var id = Guid.NewGuid();

        // Act, Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetBoatByIdAsync(id));
        Assert.Contains("not found", ex.Message);
    }

    [Fact]
    public async Task CreateBoatAsync_AddsAndReturnsMappedBoat()
    {
        // Arrange
        var context = CreateDbContext();
        var dto = new CreateBoatDto("new Boat", "new Desc");
        var service = new BoatService(context, _mapper);

        // Act
        var result = await service.CreateBoatAsync(dto);

        // Assert
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Description, result.Description);
        Assert.Single(context.Boats);
    }

    [Fact]
    public async Task UpdateBoatAsync_Updates_WhenExists()
    {
        // Arrange
        var context = CreateDbContext();
        var boat = new Boat("Old", "Old");
        context.Boats.Add(boat);
        await context.SaveChangesAsync();

        var updateDto = new UpdateBoatDto("Updated", "Updated");
        var service = new BoatService(context, _mapper);

        // Act
        await service.UpdateBoatAsync(boat.Id, updateDto);

        // Assert
        var updated = await context.Boats.FindAsync(boat.Id);
        Assert.Equal(updateDto.Name, updated?.Name);
        Assert.Equal(updateDto.Description, updated?.Description);
    }

    [Fact]
    public async Task UpdateBoatAsync_Throws_WhenNotFound()
    {
        // Arrange
        var context = CreateDbContext();
        var dto = new UpdateBoatDto("New", "New");
        var service = new BoatService(context, _mapper);

        // Act, Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateBoatAsync(Guid.NewGuid(), dto));

        // Assert
        Assert.Contains("not found", ex.Message);
    }

    [Fact]
    public async Task DeleteBoatAsync_Removes_WhenExists()
    {
        // Arrange
        var context = CreateDbContext();

        var boat = new Boat("Delete Me", "Delete Me");
        context.Boats.Add(boat);
        await context.SaveChangesAsync();

        var service = new BoatService(context, _mapper);

        // Act
        await service.DeleteBoatAsync(boat.Id);

        // Assert
        Assert.Empty(context.Boats);
    }

    [Fact]
    public async Task DeleteBoatAsync_Throws_WhenNotFound()
    {
        // Arrange
        var context = CreateDbContext();
        var service = new BoatService(context, _mapper);

        // Act, Asser
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteBoatAsync(Guid.NewGuid()));

        // Assert
        Assert.Contains("not found", ex.Message);
    }
}
