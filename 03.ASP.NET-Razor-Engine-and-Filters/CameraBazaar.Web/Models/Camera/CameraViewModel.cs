namespace CameraBazaar.Web.Models.Camera
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class CameraViewModel
    {
        public int Id { get; set; }

        [Required]
        public Make Make { get; set; }

        [Required]
        [RegularExpression(@"([A-Z0-9][-]*)+")]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(0, 100)]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        public int MinIso { get; set; }

        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        public string VideoResulution { get; set; }

        public int LightMetering { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
