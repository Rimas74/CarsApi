using CarsApi.DataStorage;
using CarsApi.Models;
using CarsApi.Repositories.Interfaces;

namespace CarsApi.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly CarsApiDbContext carsApiDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, CarsApiDbContext carsApiDbContext)
        {

            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.carsApiDbContext = carsApiDbContext;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //u[load image  to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://local:123/images/image.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            //Add the image to Images table
            await carsApiDbContext.Images.AddAsync(image);
            await carsApiDbContext.SaveChangesAsync();

            return image;
        }
    }
}
