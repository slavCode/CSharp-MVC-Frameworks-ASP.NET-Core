namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Part
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [MaxLength(int.MaxValue)]
        public int Quantity { get; set; }

        public int SupplierId { get; set; }  

        public Supplier Supplier { get; set; }

        public ICollection<PartCars> Cars { get; set; } = new List<PartCars>();

    }
}
