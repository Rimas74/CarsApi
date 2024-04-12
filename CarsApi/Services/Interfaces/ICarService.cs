using CarsApi.DTOs;

namespace CarsApi.Services.Interfaces
    {
    public interface ICarService
        {
        Task<IEnumerable<CarDto>> GetAllCarsAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null,
            bool? isAscending = true);
        Task<CarDto?> GetCarByIdAsync(int id);
        Task<IEnumerable<CarDto>> GetCarsByColorAsync(string color);
        Task<CarDto> AddCarAsync(CarDto carDto);
        Task<CarDto> UpdateCarAsync(int id, CarDto carDto);
        Task DeleteCarAsync(int id);
        }
    }
