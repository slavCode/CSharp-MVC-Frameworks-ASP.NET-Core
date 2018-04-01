namespace CameraBazaar.Data.Models
{
    public class Camera
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinShutterSpeed { get; set; }

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
