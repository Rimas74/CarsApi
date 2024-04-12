using CarsApi.Models;

namespace CarsApi.Repositories.Interfaces
    {
    public interface ICarRepository
        {
        Task<IEnumerable<Car>> GetAllCarsAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool? isAscending = true
            );
        Task<IEnumerable<Car>> GetCarsByColorAsync(string color);
        Task<Car> GetCarAsync(int id);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(int id, Car car);
        Task DeleteCarAsync(int id);
        }
    }
