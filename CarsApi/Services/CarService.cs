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

        public IEnumerable<CarDto> GetAllCars()
            {
            var cars = _carRepository.GetAllCars();
            return cars.Select(c => ToDto(c));
            }

        public CarDto GetCarById(int id)
            {
            var car = _carRepository.GetCar(id);
            return car != null ? ToDto(car) : null;
            }

        public IEnumerable<CarDto> GetCarsByColor(string color)
            {
            var cars = _carRepository.GetCarsByColor(color);
            return cars.Select(c => ToDto(c));
            }

        public CarDto AddCar(CarDto carDto)
            {
            var car = ToDomainModel(carDto);
            var addedCar = _carRepository.AddCar(car);
            return ToDto(addedCar);
            }

        public CarDto UpdateCar(int id, CarDto carDto)
            {
            var carToUpdate = ToDomainModel(carDto);
            var updatedCar = _carRepository.UpdateCar(id, carToUpdate);
            return updatedCar != null ? ToDto(updatedCar) : null;
            }

        public void DeleteCar(int id)
            {
            _carRepository.DeleteCar(id);
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
