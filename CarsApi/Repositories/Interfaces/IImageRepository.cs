using CarsApi.Models;

namespace CarsApi.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
