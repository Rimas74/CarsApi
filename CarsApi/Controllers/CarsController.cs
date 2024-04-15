using CarsApi.DTOs;
using CarsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]

    public class CarsController : ControllerBase
        {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
            {
            _carService = carService;
            }

        //GET Car
        //GET:/ api/cars?filterOn=Make&filterQuery=Toyota
        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCarsAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
            {
            var cars = await _carService.GetAllCarsAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
            return Ok(cars);
            }



        [HttpGet("{id:int}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<ActionResult<CarDto>> GetCarByIdAsync([FromRoute] int id)
            {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
                {
                return NotFound();
                }
            return Ok(car);
            }



        [HttpGet("ByColor")]
        [Authorize(Roles = "Reader")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCarByColorAsync([FromQuery] string color)
            {
            var cars = await _carService.GetCarsByColorAsync(color);
            return Ok(cars);
            }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<CarDto>> AddCarAsync([FromBody] CarDto carDto)
            {
            var newCar = await _carService.AddCarAsync(carDto);
            return Ok(newCar);
            }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<CarDto>> UpdateCarAsync([FromRoute] int id, [FromBody] CarDto carDto)
            {
            var updatedCar = await _carService.UpdateCarAsync(id, carDto);
            if (updatedCar == null)
                {
                return NotFound();
                }
            return Ok(updatedCar);
            }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] int id)
            {
            await _carService.DeleteCarAsync(id);
            return NoContent();
            }
        }
    }
