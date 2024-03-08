using System.ComponentModel.DataAnnotations;

namespace CarHireApi.DTOs
{
    public class CarUpdateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Make { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Model { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "BasePricePerDay must be greater than 0")]
        public decimal BasePricePerDay { get; set; }
    }
}
