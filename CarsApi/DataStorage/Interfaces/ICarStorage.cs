using CarsApi.Models;

namespace CarsApi.DataStorage.Interfaces
    {
    public interface ICarStorage
        {
        List<Car> Cars { get; }
        }
    }
