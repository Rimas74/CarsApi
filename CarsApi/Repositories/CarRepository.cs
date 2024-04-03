using CarsApi.DataStorage;
using CarsApi.DataStorage.Interfaces;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarsApi.Repositories
    {
    public class CarRepository : ICarRepository
        {
        

        private readonly CarsApiDbContext _context;
        private readonly ILogger<CarRepository> _logger;

        public CarRepository(CarsApiDbContext context, ILogger<CarRepository> logger)
            {
            _context = context;
            _logger = logger;
            }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
            {
            _logger.LogInformation("Retrieving all cars");
            return await _context.Cars.ToListAsync();
            }

        public async Task<IEnumerable<Car>> GetCarsByColorAsync(string color)
            {
            _logger.LogInformation($"Retrieving cars by color: {color}.");
            return await _context.Cars.Where(c => c.Color.Equals(color)).ToListAsync();
            }

        public async Task<Car> GetCarAsync(int id)
            {
            _logger.LogInformation($"Retrieving car with ID: {id}.");
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            }

        public async Task<Car> AddCarAsync(Car car)
            {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Car added with ID: {car.Id}");
            return car;
            }

        public async Task<Car> UpdateCarAsync(int id, Car updatedCar)
            {
            var car = await GetCarAsync(id);
            if (car == null)
                {
                _logger.LogWarning($"Update failed. Car with ID: {id} not found.");
                return null;
                }

            car.Make = updatedCar.Make;
            car.Model = updatedCar.Model;
            car.Color = updatedCar.Color;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Car updated with ID: {car.Id}");
            return car;
            }

        public async Task DeleteCarAsync(int id)
            {
            var car = await GetCarAsync(id);
            if (car != null)
                {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Car deleted with ID: {id}");
                }
            else
                {
                _logger.LogWarning($"Delete failed. Car with ID: {id} not found.");
                }
            }
        }

    }
