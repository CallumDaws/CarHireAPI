using CarHireApi.Models;

namespace CarHireApi.Services
{
    public class PricingService
    {
        private const decimal YoungDriverSurcharge = 32.50m;
        private const decimal SeniorDriverSurcharge = 15.00m;
        private const int YoungDriverAgeThreshold = 25;
        private const int SeniorDriverAgeThreshold = 75;

        /// <summary>
        /// Calculates the price per day for renting a car based on the driver's age.
        /// Additional surcharges are applied for young and senior drivers according to predefined age thresholds.
        /// </summary>
        /// <param name="car">The car for which the rental price is being calculated.</param>
        /// <param name="driverAge">The age of the driver renting the car.</param>
        /// <returns>The total price per day for renting the specified car, including any applicable surcharges.</returns>
        public decimal CalculatePricePerDay(Car car, int driverAge)
        {
            decimal pricePerDay = car.BasePricePerDay;

            // Apply an additional surcharge for drivers below the young driver age threshold.
            if (driverAge < YoungDriverAgeThreshold)
            {
                pricePerDay += YoungDriverSurcharge;
            }
            // Apply an additional surcharge for drivers at or above the senior driver age threshold.
            else if (driverAge >= SeniorDriverAgeThreshold)
            {
                pricePerDay += SeniorDriverSurcharge;
            }

            return pricePerDay;
        }

    }
}
