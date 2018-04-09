namespace CameraBazaar.Data.Models
{
    using Common;
    using Enums;
    using System.ComponentModel.DataAnnotations;

    public class Camera
    {
        public int Id { get; set; }

        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [RegularExpression(@"([A-Z0-9][-]*)+")]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(ValidationConstatnts.QuantityMinLength,
            ValidationConstatnts.QuantityMaxLength)]
        public int Quantity { get; set; }

        [Range(ValidationConstatnts.MinShutterSpeedMinLength,
            ValidationConstatnts.MinShutterSpeedMaxLength)]
        public int MinShutterSpeed { get; set; }

        [Range(ValidationConstatnts.MaxShutterSpeedMinLength,
            ValidationConstatnts.MaxShutterSpeedMaxLength)]
        public int MaxShutterSpeed { get; set; }

        public MinISO MinIso { get; set; }

        [Range(ValidationConstatnts.MaxIsoMinLength,
            ValidationConstatnts.MaxIsoMaxLength)]
        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        [MinLength(ValidationConstatnts.VideoResulutionMinLength)]
        [MaxLength(ValidationConstatnts.VideoResulutionMaxLength)]
        public string VideoResulution { get; set; }

        public LightMetering LightMetering { get; set; }

        [MaxLength(ValidationConstatnts.DescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(ValidationConstatnts.ImageUrlMaxLength)]
        [RegularExpression(@"^(http[s]*:\/\/).+")]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
