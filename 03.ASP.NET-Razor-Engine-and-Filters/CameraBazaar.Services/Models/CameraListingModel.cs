namespace CameraBazaar.Services.Models
{
    using System;
    using Data.Common;
    using Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class CameraListingModel
    {
        public int Id { get; set; }

        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [RegularExpression(@"([A-Z0-9][-]*)+")]
        public string Model { get; set; }

        [Range(0.0, Double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(ValidationConstants.ImageUrlMaxLength)]
        [RegularExpression(@"^(http[s]*:\/\/).+")]
        public string Image { get; set; }

        public bool InStock { get; set; }
    }
}
