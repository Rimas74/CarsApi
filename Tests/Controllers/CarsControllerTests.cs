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

<<<<<<< HEAD
    [Theory, CustomAutoData]
    public async Task GetAllCarsAsync_ReturnsAllCars(CarDto carDto)
    {
        // Arrange
        var mockCars = new List<CarDto> { carDto };
    }
=======
    [Fact]
    public async Task GetAllCarsAsync_ReturnsAllCars()
        {
        // Arrange
        var mockCars = new List<CarDto> { new CarDto { Make = "Toyota", Model = "Corolla", Color = "Red" } };
        _mockCarService.Setup(s => s.GetAllCarsAsync(null, null, null, true)).ReturnsAsync(mockCars);

        // Act
        var result = await _controller.GetAllCarsAsync(null, null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CarDto>>(okResult.Value);
        Assert.Single(returnValue);
        _mockCarService.Verify(s => s.GetAllCarsAsync(null, null, null, true), Times.Once);
        }

<<<<<<< HEAD
    [Theory, CustomAutoData]
    public async Task GetCarByIdAsync_ReturnsCar_WhenCarExists(CarDto carDto)
        {
        // Arrange
        //var carDto = new CarDto { Make = "Toyota", Model = "Corolla", Color = "Red" };
=======
    [Fact]
    public async Task GetCarByIdAsync_ReturnsCar_WhenCarExists()
        {
        // Arrange
        var carDto = new CarDto { Make = "Toyota", Model = "Corolla", Color = "Red" };
        int carId = 1;
        _mockCarService.Setup(s => s.GetCarByIdAsync(carId)).ReturnsAsync(carDto);

        // Act
        var result = await _controller.GetCarByIdAsync(carId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(carDto, okResult.Value);
        }

<<<<<<< HEAD
    [Theory, CustomAutoData]
    public async Task GetCarByIdAsync_ReturnsNotFound_WhenCarDoesNotExist(CarDto carDto)
=======
    [Fact]
    public async Task GetCarByIdAsync_ReturnsNotFound_WhenCarDoesNotExist()
        {
        int carId = 1;
        _mockCarService.Setup(s => s.GetCarByIdAsync(carId)).ReturnsAsync((CarDto)null);

        var result = await _controller.GetCarByIdAsync(carId);

        Assert.IsType<NotFoundResult>(result.Result);
        }
<<<<<<< HEAD
    [Theory, CustomAutoData]
    public async Task UpdateCarAsync_ReturnsUpdatedCarDto_WhenUpdateIsSuccessful(CarDto carDto)
        {
        // Arrange

=======
    [Fact]
    public async Task UpdateCarAsync_ReturnsUpdatedCarDto_WhenUpdateIsSuccessful()
        {
        // Arrange
        var carDto = new CarDto { Make = "Honda", Model = "Civic", Color = "Blue" };
        int carId = 1;
        _mockCarService.Setup(s => s.UpdateCarAsync(carId, carDto)).ReturnsAsync(carDto);

        // Act
        var result = await _controller.UpdateCarAsync(carId, carDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(carDto, okResult.Value);
        }

<<<<<<< HEAD
    [Theory, CustomAutoData]
    public async Task UpdateCarAsync_ReturnsNotFound_WhenCarDoesNotExist(CarDto carDto)
        {

=======
    [Fact]
    public async Task UpdateCarAsync_ReturnsNotFound_WhenCarDoesNotExist()
        {
        var carDto = new CarDto { Make = "Honda", Model = "Civic", Color = "Blue" };
        int carId = 1;
        _mockCarService.Setup(s => s.UpdateCarAsync(carId, carDto)).ReturnsAsync((CarDto)null);

        var result = await _controller.UpdateCarAsync(carId, carDto);

        Assert.IsType<NotFoundResult>(result.Result);
        }
<<<<<<< HEAD
    [Theory, CustomAutoData]
=======
    [Fact]
    public async Task DeleteCarAsync_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
        int carId = 1;
        _mockCarService.Setup(s => s.DeleteCarAsync(carId)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteCarAsync(carId);

        Assert.IsType<NoContentResult>(result);
        _mockCarService.Verify(s => s.DeleteCarAsync(carId), Times.Once);
        }
<<<<<<< HEAD
    [Theory, CustomAutoData]
    public async Task AddCarAsync_ReturnsAddedCarDto_WhenAdditionIsSuccessful(CarDto carDto)
        {
        // Arrange

=======
    [Fact]
    public async Task AddCarAsync_ReturnsAddedCarDto_WhenAdditionIsSuccessful()
        {
        // Arrange
        var carDto = new CarDto { Make = "Honda", Model = "Civic", Color = "Blue" };
        _mockCarService.Setup(s => s.AddCarAsync(carDto)).ReturnsAsync(carDto);

        // Act
        var result = await _controller.AddCarAsync(carDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCarDto = okResult.Value as CarDto;
        Assert.NotNull(returnedCarDto);
<<<<<<< HEAD
        Assert.Equal(carDto.Make, returnedCarDto.Make);
        Assert.Equal(carDto.Model, returnedCarDto.Model);
        Assert.Equal(carDto.Color, returnedCarDto.Color);

        }

    [Theory, CustomAutoData]
=======
        Assert.Equal("Honda", returnedCarDto.Make);
        Assert.Equal("Civic", returnedCarDto.Model);
        Assert.Equal("Blue", returnedCarDto.Color);

        }

    [Fact]
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

