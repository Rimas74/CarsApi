using AutoMapper;
using CarsApi.DTOs;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;
using CarsApi.Services.Interfaces;

namespace CarsApi.Services
    {
    public class CarService : ICarService
        {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
            {
            _carRepository = carRepository;
            _mapper = mapper;
            }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync(string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool? isAscending = true)
            {
            var cars = await _carRepository.GetAllCarsAsync(filterOn, filterQuery, sortBy, isAscending);
            return _mapper.Map<IEnumerable<CarDto>>(cars);
            }

        public async Task<CarDto?> GetCarByIdAsync(int id)
            {
            var car = await _carRepository.GetCarAsync(id);
            return car != null ? _mapper.Map<CarDto>(car) : null;
            }

        public async Task<IEnumerable<CarDto>> GetCarsByColorAsync(string color)
            {
            var cars = await _carRepository.GetCarsByColorAsync(color);
            return _mapper.Map<IEnumerable<CarDto>>(cars);
            }

        public async Task<CarDto> AddCarAsync(CarDto carDto)
            {
            var car = _mapper.Map<Car>(carDto);
            var addedCar = await _carRepository.AddCarAsync(car);
            return _mapper.Map<CarDto>(addedCar);
            }

        public async Task<CarDto> UpdateCarAsync(int id, CarDto carDto)
            {
            var carToUpdate = _mapper.Map<Car>(carDto);
            var updatedCar = await _carRepository.UpdateCarAsync(id, carToUpdate);
            return updatedCar != null ? _mapper.Map<CarDto>(updatedCar) : null;
            }

        public async Task DeleteCarAsync(int id)
            {
            await _carRepository.DeleteCarAsync(id);
            }

        //private static CarDto ToDto(Car car)
        //    {
        //    return new CarDto
        //        {
        //        Make = car.Make,
        //        Model = car.Model,
        //        Color = car.Color
        //        };
        //    }

        //private static Car ToDomainModel(CarDto dto)
        //    {
        //    return new Car
        //        {
        //        Make = dto.Make,
        //        Model = dto.Model,
        //        Color = dto.Color
        //        };
        //    }
        }
    }
