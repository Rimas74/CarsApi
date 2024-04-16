using System.Collections.Generic;
using System.Threading.Tasks;
using CarsApi.Controllers;
using CarsApi.DTOs;
using CarsApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class CarsControllerTests
    {
    private readonly Mock<ICarService> _mockCarService;
    private readonly CarsController _controller;


    public CarsControllerTests()
        {
        _mockCarService = new Mock<ICarService>();
        _controller = new CarsController(_mockCarService.Object);
        }

    [Theory, CustomAutoData]
    public async Task GetAllCarsAsync_ReturnsAllCars(CarDto carDto)
        {
        // Arrange
        var mockCars = new List<CarDto> { carDto };
        _mockCarService.Setup(s => s.GetAllCarsAsync(null, null, null, true)).ReturnsAsync(mockCars);

        // Act
        var result = await _controller.GetAllCarsAsync(null, null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CarDto>>(okResult.Value);
        Assert.Single(returnValue);
        _mockCarService.Verify(s => s.GetAllCarsAsync(null, null, null, true), Times.Once);
        }

    [Theory, CustomAutoData]
    public async Task GetCarByIdAsync_ReturnsCar_WhenCarExists(CarDto carDto)
        {
        // Arrange
        //var carDto = new CarDto { Make = "Toyota", Model = "Corolla", Color = "Red" };
        int carId = 1;
        _mockCarService.Setup(s => s.GetCarByIdAsync(carId)).ReturnsAsync(carDto);

        // Act
        var result = await _controller.GetCarByIdAsync(carId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(carDto, okResult.Value);
        }

    [Theory, CustomAutoData]
    public async Task GetCarByIdAsync_ReturnsNotFound_WhenCarDoesNotExist(CarDto carDto)
        {
        int carId = 1;
        _mockCarService.Setup(s => s.GetCarByIdAsync(carId)).ReturnsAsync((CarDto)null);

        var result = await _controller.GetCarByIdAsync(carId);

        Assert.IsType<NotFoundResult>(result.Result);
        }
    [Theory, CustomAutoData]
    public async Task UpdateCarAsync_ReturnsUpdatedCarDto_WhenUpdateIsSuccessful(CarDto carDto)
        {
        // Arrange

        int carId = 1;
        _mockCarService.Setup(s => s.UpdateCarAsync(carId, carDto)).ReturnsAsync(carDto);

        // Act
        var result = await _controller.UpdateCarAsync(carId, carDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(carDto, okResult.Value);
        }

    [Theory, CustomAutoData]
    public async Task UpdateCarAsync_ReturnsNotFound_WhenCarDoesNotExist(CarDto carDto)
        {

        int carId = 1;
        _mockCarService.Setup(s => s.UpdateCarAsync(carId, carDto)).ReturnsAsync((CarDto)null);

        var result = await _controller.UpdateCarAsync(carId, carDto);

        Assert.IsType<NotFoundResult>(result.Result);
        }
    [Theory, CustomAutoData]
    public async Task DeleteCarAsync_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
        int carId = 1;
        _mockCarService.Setup(s => s.DeleteCarAsync(carId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteCarAsync(carId);

        Assert.IsType<NoContentResult>(result);
        _mockCarService.Verify(s => s.DeleteCarAsync(carId), Times.Once);
        }
    [Theory, CustomAutoData]
    public async Task AddCarAsync_ReturnsAddedCarDto_WhenAdditionIsSuccessful(CarDto carDto)
        {
        // Arrange

        _mockCarService.Setup(s => s.AddCarAsync(carDto)).ReturnsAsync(carDto);

        // Act
        var result = await _controller.AddCarAsync(carDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCarDto = okResult.Value as CarDto;
        Assert.NotNull(returnedCarDto);
        Assert.Equal(carDto.Make, returnedCarDto.Make);
        Assert.Equal(carDto.Model, returnedCarDto.Model);
        Assert.Equal(carDto.Color, returnedCarDto.Color);

        }

    [Theory, CustomAutoData]
    public async Task AddCarAsync_ReturnsBadRequest_WhenCarDtoIsNull()
        {
        // Arrange
        CarDto carDto = null;

        // Act
        var result = await _controller.AddCarAsync(carDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.NotNull(badRequestResult);
        }


    }

