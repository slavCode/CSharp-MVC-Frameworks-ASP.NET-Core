namespace CameraBazaar.Services.Models
{
    using Data.Common;
    using Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CameraDetailsModel
    {
        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [RegularExpression(@"([A-Z0-9][-]*)+")]
        public string Model { get; set; }

        [Range(0.0, Double.MaxValue)]
        public decimal Price { get; set; }

        [Range(ValidationConstants.QuantityMinLength,
            ValidationConstants.QuantityMaxLength)]
        public int Quantity { get; set; }

        [Range(ValidationConstants.MinShutterSpeedMinLength,
            ValidationConstants.MinShutterSpeedMaxLength)]
        public int MinShutterSpeed { get; set; }

        [Range(ValidationConstants.MaxShutterSpeedMinLength,
            ValidationConstants.MaxShutterSpeedMaxLength)]
        public int MaxShutterSpeed { get; set; }

        public MinISO MinIso { get; set; }

        [Range(ValidationConstants.MaxIsoMinLength,
            ValidationConstants.MaxIsoMaxLength)]
        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        [MinLength(ValidationConstants.VideoResulutionMinLength)]
        [MaxLength(ValidationConstants.VideoResulutionMaxLength)]
        public string VideoResulution { get; set; }

        public LightMetering? LightMetering { get; set; }

        [MaxLength(ValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(ValidationConstants.ImageUrlMaxLength)]
        [RegularExpression(@"^(http[s]*:\/\/).+")]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
