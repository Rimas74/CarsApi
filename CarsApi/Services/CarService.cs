using CarsApi.DTOs;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;
using CarsApi.Services.Interfaces;

namespace CarsApi.Services
    {
    public class CarService : ICarService
        {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
            {
            _carRepository = carRepository;
            }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
            {
            var cars = await _carRepository.GetAllCarsAsync();
            return cars.Select(c => ToDto(c));
            }

        public async Task<CarDto> GetCarByIdAsync(int id)
            {
            var car = await _carRepository.GetCarAsync(id);
            return car != null ? ToDto(car) : null;
            }

        public async Task<IEnumerable<CarDto>> GetCarsByColorAsync(string color)
            {
            var cars = await _carRepository.GetCarsByColorAsync(color);
            return cars.Select(c => ToDto(c));
            }

        public async Task<CarDto> AddCarAsync(CarDto carDto)
            {
            var car = ToDomainModel(carDto);
            var addedCar = await _carRepository.AddCarAsync(car);
            return ToDto(addedCar);
            }

        public async Task<CarDto> UpdateCarAsync(int id, CarDto carDto)
            {
            var carToUpdate = ToDomainModel(carDto);
            var updatedCar = await _carRepository.UpdateCarAsync(id, carToUpdate);
            return updatedCar != null ? ToDto(updatedCar) : null;
            }

        public async Task DeleteCarAsync(int id)
            {
            await _carRepository.DeleteCarAsync(id);
            }

        private static CarDto ToDto(Car car)
            {
            return new CarDto
                {
                Make = car.Make,
                Model = car.Model,
                Color = car.Color
                };
            }

        private static Car ToDomainModel(CarDto dto)
            {
            return new Car
                {
                Make = dto.Make,
                Model = dto.Model,
                Color = dto.Color
                };
            }
        }
    }
