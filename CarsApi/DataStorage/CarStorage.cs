using CarsApi.DataStorage.Interfaces;
using CarsApi.Models;

namespace CarsApi.DataStorage
    {
    public class CarStorage : ICarStorage
        {
        public List<Car> Cars { get; private set; }

        public CarStorage()
            {
            Cars = new List<Car>();
            }
        }
    }
