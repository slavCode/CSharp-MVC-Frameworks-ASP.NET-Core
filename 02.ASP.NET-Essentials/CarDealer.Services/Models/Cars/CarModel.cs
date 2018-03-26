namespace CarDealer.Services.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Model { get; set; }

        [Display(Name = "Travelled Distance")]
        [Range(0, long.MaxValue)]
        public long TravelledDistance { get; set; }
    }
}
