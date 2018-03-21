namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
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

        [MinLength(0)]
        [MaxLength(int.MaxValue)]
        public long TravelledDistance { get; set; }

        public ICollection<PartCars> Parts { get; set; } = new List<PartCars>();

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    }
}
