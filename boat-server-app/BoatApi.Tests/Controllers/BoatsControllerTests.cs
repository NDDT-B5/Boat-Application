using AutoMapper;
using BoatApi.Controllers;
using BoatApi.DTOs.Boat;
using BoatApi.Models;
using BoatApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BoatApi.Tests.Controllers;

public class BoatsControllerTests
{
    private readonly Mock<IBoatService> _mockBoatService = new();
    private readonly BoatsController _controller;

    public BoatsControllerTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Boat, BoatDto>();
        });
        var mapper = config.CreateMapper();

        _controller = new BoatsController(mapper, _mockBoatService.Object);
    }

    [Fact]
    public async Task GetBoatsAsync_ReturnsOkWithBoats()
    {
        // Arrange
        var boats = new List<BoatDto>
        {
            new(Guid.NewGuid(), "Boat 1", "Desc 1"),
            new(Guid.NewGuid(), "Boat 2", "Desc 2")
        };

        _mockBoatService.Setup(s => s.GetAllBoatsAsync())
                        .ReturnsAsync(boats);

        // Act
        var result = await _controller.GetBoatsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBoats = Assert.IsAssignableFrom<IEnumerable<BoatDto>>(okResult.Value);
        Assert.Equal(2, returnedBoats.Count());

        _mockBoatService.Verify(s => s.GetAllBoatsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetBoat_ReturnsOkWithBoat_WhenFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var boat = new BoatDto(id, "Boat 1", "Desc 1");

        _mockBoatService.Setup(s => s.GetBoatByIdAsync(id))
                        .ReturnsAsync(boat);

        // Act
        var result = await _controller.GetBoat(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBoat = Assert.IsType<BoatDto>(okResult.Value);
        Assert.Equal(id, returnedBoat.Id);

        _mockBoatService.Verify(s => s.GetBoatByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task CreateBoat_ReturnsCreatedAtAction()
    {
        // Arrange
        var dto = new CreateBoatDto("Boat X", "Desc X");
        var createdBoat = new BoatDto(Guid.NewGuid(), dto.Name, dto.Description);

        _mockBoatService.Setup(s => s.CreateBoatAsync(dto))
                        .ReturnsAsync(createdBoat);

        // Act
        var result = await _controller.CreateBoat(dto);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returned = Assert.IsType<BoatDto>(createdAt.Value);
        Assert.Equal(dto.Name, returned.Name);
        Assert.Equal(dto.Description, returned.Description);

        _mockBoatService.Verify(s => s.CreateBoatAsync(dto), Times.Once);
    }

    [Fact]
    public async Task UpdateBoat_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new UpdateBoatDto("Updated Name", "Updated Desc");

        _mockBoatService.Setup(s => s.UpdateBoatAsync(id, dto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateBoat(id, dto);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _mockBoatService.Verify(s => s.UpdateBoatAsync(id, dto), Times.Once);
    }

    [Fact]
    public async Task DeleteBoat_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();

        _mockBoatService.Setup(s => s.DeleteBoatAsync(id)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteBoat(id);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _mockBoatService.Verify(s => s.DeleteBoatAsync(id), Times.Once);
    }
}
