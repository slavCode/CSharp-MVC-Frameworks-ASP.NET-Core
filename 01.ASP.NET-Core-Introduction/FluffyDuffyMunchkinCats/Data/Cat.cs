namespace FluffyDuffyMunchkinCats.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        private const int MaxLength = 50;
        private const int MinLength = 2;

        public int Id { get; set; }

        [Required]
        [MinLength(MinLength)]
        [MaxLength(MaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinLength)]
        [MaxLength(MaxLength)]
        public string Breed { get; set; }

        public int Age { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string ImageUrl { get; set; }
     }
}
