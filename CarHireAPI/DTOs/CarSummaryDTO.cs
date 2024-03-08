namespace CarHireApi.DTOs
{
    public class CarSummaryDTO
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
    }

}
