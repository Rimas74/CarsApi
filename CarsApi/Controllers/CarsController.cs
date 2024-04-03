using CarsApi.DTOs;
using CarsApi.Services.Interfaces;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCarsAsync()
            {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
            }

        [HttpGet("{id:int}")]
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
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCarByColorAsync([FromQuery] string color)
            {
            var cars = await _carService.GetCarsByColorAsync(color);
            return Ok(cars);
            }

        [HttpPost]
        public async Task<ActionResult<CarDto>> AddCarAsync([FromBody] CarDto carDto)
            {
            var newCar = await _carService.AddCarAsync(carDto);
            return Ok(newCar);
            }

        [HttpPut("{id}")]
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
        public async Task<IActionResult> DeleteCarAsync([FromRoute] int id)
            {
            await _carService.DeleteCarAsync(id);
            return NoContent();
            }
        }
    }
