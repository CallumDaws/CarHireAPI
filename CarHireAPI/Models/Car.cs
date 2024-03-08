namespace CarHireApi.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal BasePricePerDay { get; set; }
        public int MinAgeRequirement { get; set; } = 23; // Default minimum age requirement
        public int MaxAgeRequirement { get; set; } = 99; // Default maximum age requirement
    }
}
