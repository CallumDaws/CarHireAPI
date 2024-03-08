using CarHireApi.Models;
using CarHireApi.DTOs;
using CarHireApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarHireApi.Controllers
{
    /// <summary>
    /// Controls operations related to car hires, including listing available cars based on age criteria,
    /// modifying car details, and deleting cars from the list.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CarHireController : ControllerBase
    {
        // Static list of cars to simulate database storage for the simplicity of this example.
        private static readonly List<Car> _cars = new()
        {
            new Car { Id = 1, Make = "Toyota", Model = "Corolla", BasePricePerDay = 25.00m, MinAgeRequirement = 23, MaxAgeRequirement = 99 },
            new Car { Id = 2, Make = "Ford", Model = "Fiesta", BasePricePerDay = 30.00m, MinAgeRequirement = 23, MaxAgeRequirement = 99 },
            new Car { Id = 3, Make = "BMW", Model = "3 Series", BasePricePerDay = 55.00m, MinAgeRequirement = 25, MaxAgeRequirement = 80 },
            new Car { Id = 4, Make = "Tesla", Model = "Model 3", BasePricePerDay = 70.00m, MinAgeRequirement = 25, MaxAgeRequirement = 75 },
            new Car { Id = 5, Make = "Volkswagen", Model = "Golf", BasePricePerDay = 40.00m, MinAgeRequirement = 23, MaxAgeRequirement = 85 },
            new Car { Id = 6, Make = "Honda", Model = "Civic", BasePricePerDay = 35.00m, MinAgeRequirement = 23, MaxAgeRequirement = 90 },
        };

        private readonly PricingService _pricingService = new();

        /// <summary>
        /// Retrieves a list of cars available for hire that meet the age criteria provided by the user.
        /// Applies dynamic pricing based on the driver's age.
        /// </summary>
        /// <param name="driverAge">The age of the driver to filter available cars and calculate prices.</param>
        /// <returns>A list of cars along with their calculated hire price per day.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CarSummaryDTO>> GetAvailableCars(int driverAge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var availableCars = _cars
                .Where(car => driverAge >= car.MinAgeRequirement && driverAge <= car.MaxAgeRequirement)
                .Select(car => new CarSummaryDTO
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    PricePerDay = _pricingService.CalculatePricePerDay(car, driverAge)
                })
                .ToList();

            if (!availableCars.Any())
            {
                return NotFound("No available cars meet the age requirements.");
            }

            return Ok(availableCars);
        }

        /// <summary>
        /// Modifies the details of an existing car based on the provided ID and update information.
        /// Note: Only certain fields (Make, Model, BasePricePerDay) can be updated through this method.
        /// </summary>
        /// <param name="id">The ID of the car to modify.</param>
        /// <param name="updatedCarDetails">The new details for the car.</param>
        /// <returns>An updated car summary or an error message if the car is not found.</returns>
        [HttpPut("{id}")]
        public IActionResult ModifyCar(int id, [FromBody] CarUpdateDTO updatedCarDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound("Car not found.");
            }

            car.Make = updatedCarDetails.Make;
            car.Model = updatedCarDetails.Model;
            car.BasePricePerDay = updatedCarDetails.BasePricePerDay;

            return Ok(new CarSummaryDTO
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                PricePerDay = car.BasePricePerDay
            });
        }

        /// <summary>
        /// Deletes a car from the list based on its ID.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>A confirmation message or an error message if the car is not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound("Car not found.");
            }

            _cars.Remove(car);
            return Ok($"Car with ID {id} has been deleted successfully.");
        }

    }
}
