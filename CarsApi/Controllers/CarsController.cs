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
        public ActionResult<IEnumerable<CarDto>> GetAllCars()
            {
            var cars = _carService.GetAllCars();
            return Ok(cars);
            }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<CarDto> GetCarById([FromRoute] int id)
            {
            var car = _carService.GetCarById(id);
            if (car == null)
                {
                return NotFound();
                }
            return Ok(car);
            }
        [HttpGet("ByColor")]
        public ActionResult<IEnumerable<CarDto>> GetCarByColor([FromQuery] string color)
            {
            var cars = _carService.GetCarsByColor(color);
            return Ok(cars);
            }
        [HttpPost]
        public ActionResult<CarDto> AddCar([FromBody] CarDto carDto)
            {
            var newCar = _carService.AddCar(carDto);
            return Ok(newCar);
            }
        [HttpPut("{id}")]
        public ActionResult<CarDto> UpdateCar([FromRoute] int id, [FromBody] CarDto carDto)
            {
            var updatedCar = _carService.UpdateCar(id, carDto);
            if (updatedCar == null)
                {
                return NotFound();
                }
            return Ok(updatedCar);
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar([FromRoute] int id)
            {
            _carService.DeleteCar(id);
            return NoContent();
            }
        }
    }
