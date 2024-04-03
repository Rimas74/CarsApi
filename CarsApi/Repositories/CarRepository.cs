using CarsApi.DataStorage.Interfaces;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;

namespace CarsApi.Repositories
    {
    public class CarRepository : ICarRepository
        {
        private readonly ICarStorage _carStorage;
        private int _nextId = 1;

        public CarRepository(ICarStorage carStorage)
            {
            _carStorage = carStorage;
            }

        public IEnumerable<Car> GetAllCars()
            {
            return _carStorage.Cars;
            }

        public IEnumerable<Car> GetCarsByColor(string color)
            {
            return _carStorage.Cars.Where(c => c.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            }

        public Car GetCar(int id)
            {
            return _carStorage.Cars.FirstOrDefault(c => c.Id == id);
            }

        public Car AddCar(Car car)
            {
            car.Id = _nextId++;
            _carStorage.Cars.Add(car);
            return car;
            }

        public Car UpdateCar(int id, Car car)
            {
            var carToUpdate = GetCar(id);
            if (carToUpdate == null)
                {
                return null;
                }

            carToUpdate.Make = car.Make;
            carToUpdate.Model = car.Model;
            carToUpdate.Color = car.Color;

            return carToUpdate;
            }

        public void DeleteCar(int id)
            {
            var car = GetCar(id);
            if (car != null)
                {
                _carStorage.Cars.Remove(car);
                }
            }
        }

    }
