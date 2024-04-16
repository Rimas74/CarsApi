using AutoMapper;
using CarsApi.DTOs;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;
using CarsApi.Services;
using Moq;

public class CarServiceTests
    {
    private readonly Mock<ICarRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CarService _carService;

    public CarServiceTests()
        {
        _mockRepo = new Mock<ICarRepository>();
        _mockMapper = new Mock<IMapper>();
        _carService = new CarService(_mockRepo.Object, _mockMapper.Object);
        }
    [Theory, CustomAutoData]
    public async Task GetAllCarsAsync_ReturnsAllCars(IEnumerable<Car> cars, IEnumerable<CarDto> carDtos)
        {
        // Arrange
        _mockRepo.Setup(x => x.GetAllCarsAsync(null, null, null, true)).ReturnsAsync(cars);
        _mockMapper.Setup(x => x.Map<IEnumerable<CarDto>>(cars)).Returns(carDtos);

        // Act
        var result = await _carService.GetAllCarsAsync();

        // Assert
        _mockRepo.Verify(x => x.GetAllCarsAsync(null, null, null, true), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<CarDto>>(cars), Times.Once);
        Assert.Equal(carDtos, result);
        }

    [Theory, CustomAutoData]
    public async Task GetCarByIdAsync_ReturnsCar_WhenCarExists(Car car, CarDto carDto)
        {
        // Arrange
        _mockRepo.Setup(x => x.GetCarAsync(car.Id)).ReturnsAsync(car);
        _mockMapper.Setup(x => x.Map<CarDto>(car)).Returns(carDto);

        // Act
        var result = await _carService.GetCarByIdAsync(car.Id);

        // Assert
        Assert.Equal(carDto, result);
        }

    [Theory, CustomAutoData]
    public async Task GetCarsByColorAsync_ReturnsCarsByColor(string color, IEnumerable<Car> cars, IEnumerable<CarDto> carDtos)
        {
        // Arrange
        _mockRepo.Setup(x => x.GetCarsByColorAsync(color)).ReturnsAsync(cars);
        _mockMapper.Setup(x => x.Map<IEnumerable<CarDto>>(cars)).Returns(carDtos);

        // Act
        var result = await _carService.GetCarsByColorAsync(color);

        // Assert
        Assert.Equal(carDtos, result);
        }
    [Theory, CustomAutoData]
    public async Task AddCarAsync_ReturnsAddedCarDto(CarDto carDto)
        {
        // Arrange
        var car = new Car { Make = carDto.Make, Model = carDto.Model };
        _mockMapper.Setup(x => x.Map<Car>(carDto)).Returns(car);
        _mockRepo.Setup(x => x.AddCarAsync(car)).ReturnsAsync(car);
        _mockMapper.Setup(x => x.Map<CarDto>(car)).Returns(carDto);

        // Act
        var result = await _carService.AddCarAsync(carDto);

        // Assert
        Assert.Equal(carDto, result);
        }
    [Theory, CustomAutoData]
    public async Task UpdateCarAsync_ReturnsUpdatedCarDto(CarDto carDto, Car car)
        {
        // Arrange
        _mockRepo.Setup(x => x.UpdateCarAsync(car.Id, car)).ReturnsAsync(car);
        _mockMapper.Setup(x => x.Map<Car>(carDto)).Returns(car);
        _mockMapper.Setup(x => x.Map<CarDto>(car)).Returns(carDto);

        // Act
        var result = await _carService.UpdateCarAsync(car.Id, carDto);

        // Assert
        Assert.Equal(carDto, result);
        }

    [Theory, CustomAutoData]
    public async Task DeleteCarAsync_CallsDeleteMethod(int carId)
        {
        // Act
        await _carService.DeleteCarAsync(carId);

        // Assert
        _mockRepo.Verify(x => x.DeleteCarAsync(carId), Times.Once);
        }
    }
