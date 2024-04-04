using AutoMapper;
using CarsApi.DTOs;
using CarsApi.Models;

namespace CarsApi.Profiles
    {
    public class MappingProfile : Profile
        {
        public MappingProfile()
            {
            CreateMap<Car, CarDto>().ReverseMap();
            }
        }
    }
