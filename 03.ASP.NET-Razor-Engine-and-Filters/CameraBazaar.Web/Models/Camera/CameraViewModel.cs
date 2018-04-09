namespace CameraBazaar.Web.Models.Camera
{
    using Data.Common;
    using Data.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CameraViewModel
    {
        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [RegularExpression(@"([A-Z0-9][-]*)+")]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(ValidationConstatnts.QuantityMinLength,
               ValidationConstatnts.QuantityMaxLength)]
        public int Quantity { get; set; }

        [Display(Name = "Min Shutter Speed")]
        [Range(ValidationConstatnts.MinShutterSpeedMinLength,
                   ValidationConstatnts.MaxShutterSpeedMaxLength)]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        [Range(ValidationConstatnts.MaxShutterSpeedMinLength, 
            ValidationConstatnts.MaxShutterSpeedMaxLength)]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinISO MinIso { get; set; }

        [Display(Name = "Max ISO")]
        [Range(ValidationConstatnts.MaxIsoMinLength, 
               ValidationConstatnts.MaxIsoMaxLength)]
        public int MaxIso { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Video Resolution")]
        [MinLength(ValidationConstatnts.VideoResulutionMinLength)] 
        [MaxLength(ValidationConstatnts.VideoResulutionMaxLength)]
        public string VideoResulution { get; set; }

        [Display(Name = "Light Metering")]
        public IEnumerable<LightMetering> LightMetering { get; set; }

        [MaxLength(ValidationConstatnts.DescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [MaxLength(ValidationConstatnts.ImageUrlMaxLength)]
        [RegularExpression(@"^(http[s]*:\/\/).+")]
        public string ImageUrl { get; set; }
    }
}
