using CarsApi.DTOs;

namespace CarsApi.Services.Interfaces
    {
    public interface ICarService
        {
        IEnumerable<CarDto> GetAllCars();
        CarDto GetCarById(int id);
        IEnumerable<CarDto> GetCarsByColor(string color);
        CarDto AddCar(CarDto carDto);
        CarDto UpdateCar(int id, CarDto carDto);
        void DeleteCar(int id);
        }
    }
