
using CarsApi.Repositories.Interfaces;
using CarsApi.Repositories;
using CarsApi.Services.Interfaces;
using CarsApi.Services;
using CarsApi.DataStorage.Interfaces;
using CarsApi.DataStorage;
using Microsoft.EntityFrameworkCore;

namespace CarsApi
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CarsApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            //builder.Services.AddSingleton<ICarStorage, CarStorage>();

            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService, CarService>();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
                {
                app.UseSwagger();
                app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            }
        }
    }
