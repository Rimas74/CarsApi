
using CarsApi.Repositories.Interfaces;
using CarsApi.Repositories;
using CarsApi.Services.Interfaces;
using CarsApi.Services;
using CarsApi.DataStorage.Interfaces;
using CarsApi.DataStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

            builder.Services.AddDbContext<CarAPIAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIAuthDbConnectionString")));

            //CarAPIAuthDbContext

            //builder.Services.AddSingleton<ICarStorage, CarStorage>();

            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                    {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    }
                );

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
                {
                app.UseSwagger();
                app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            }
        }
    }
