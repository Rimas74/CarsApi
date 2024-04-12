using CarsApi.DataStorage;
using CarsApi.DataStorage.Interfaces;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

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

        public async Task<IEnumerable<Car>> GetAllCarsAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool? isAscending = true
            )
            {
            IQueryable<Car> query = _context.Cars;

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
                {

                if (filterOn.Equals("Make", StringComparison.OrdinalIgnoreCase))
                    {
                    query = query.Where(x => x.Make.Contains(filterQuery));
                    }

                else if (filterOn.Equals("Model", StringComparison.OrdinalIgnoreCase))
                    {
                    query = query.Where(x => x.Model.Contains(filterQuery));
                    }
                else if (filterOn.Equals("Color", StringComparison.OrdinalIgnoreCase))
                    {
                    query = query.Where(x => x.Color.Contains(filterQuery));
                    }

                //var propertyInfo = typeof(Car).GetProperty(filterOn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                //if (propertyInfo != null)
                //    {
                //    // Building a dynamic lambda expression
                //    var parameter = Expression.Parameter(typeof(Car), "car");
                //    var property = Expression.Property(parameter, propertyInfo);
                //    var constant = Expression.Constant(filterQuery);
                //    var equals = Expression.Equal(property, constant);
                //    var lambda = Expression.Lambda<Func<Car, bool>>(equals, parameter);

                //    query = query.Where(lambda);

                }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
                {
                if (sortBy.Equals("Make", StringComparison.OrdinalIgnoreCase))
                    {
                    query = (bool)isAscending ? query.OrderBy(x => x.Make) : query.OrderByDescending(x => x.Make);
                    }
                else if (sortBy.Equals("Model", StringComparison.OrdinalIgnoreCase))
                    {
                    query = (bool)isAscending ? query.OrderBy(x => x.Model) : query.OrderByDescending(x => x.Model);
                    }
                else if (sortBy.Equals("Color", StringComparison.OrdinalIgnoreCase))
                    {
                    query = (bool)isAscending ? query.OrderBy(x => x.Color) : query.OrderByDescending(x => x.Color);
                    }
                }

            _logger.LogInformation("Retrieving cars with specified filters.");
            return await query.ToListAsync();
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

        public async Task<Car?> UpdateCarAsync(int id, Car updatedCar)
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
