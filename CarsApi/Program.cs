
using CarsApi.Repositories.Interfaces;
using CarsApi.Repositories;
using CarsApi.Services.Interfaces;
using CarsApi.Services;
using CarsApi.DataStorage.Interfaces;
using CarsApi.DataStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Xml;

namespace CarsApi
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = " Cars API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                         new OpenApiSecurityScheme
                             {
                             Reference=new OpenApiReference
                                 {
                                 Type=ReferenceType.SecurityScheme,
                                 Id=JwtBearerDefaults.AuthenticationScheme
                                 },
                             Scheme="Oauth2",
                             Name=JwtBearerDefaults.AuthenticationScheme,
                             In=ParameterLocation.Header
                             },
                         new List <string>()
                    }
                  });
            });


            builder.Services.AddDbContext<CarsApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            builder.Services.AddDbContext<CarAPIAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIAuthDbConnectionString")));

            //CarAPIAuthDbContext

            //builder.Services.AddSingleton<ICarStorage, CarStorage>();

            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService, CarService>();

            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CarsApi")
                    .AddEntityFrameworkStores<CarAPIAuthDbContext>()
                    .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
                ;

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
