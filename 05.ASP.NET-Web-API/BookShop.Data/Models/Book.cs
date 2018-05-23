namespace BookShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataModelValidationConstants;

    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Copies { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Edition { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public List<CategoryBooks> Categories { get; set; } = new List<CategoryBooks>();
    }
}
