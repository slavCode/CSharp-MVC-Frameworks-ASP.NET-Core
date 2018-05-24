namespace BookShop.Api.Models.Book
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.DataModelValidationConstants;

    public class BookRequestModel
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

        [Range(3, 21)]
        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
