using CarsApi.Models;

namespace CarsApi.Repositories.Interfaces
    {
    public interface ICarRepository
        {
        IEnumerable<Car> GetAllCars();
        IEnumerable<Car> GetCarsByColor(string color);
        Car GetCar(int id);
        Car AddCar(Car car);
        Car UpdateCar(int id, Car car);
        void DeleteCar(int id);
        }
    }
