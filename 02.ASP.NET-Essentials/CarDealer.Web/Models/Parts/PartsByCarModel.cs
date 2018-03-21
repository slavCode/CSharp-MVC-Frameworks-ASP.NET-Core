namespace CarDealer.Web.Models.Parts
{
    using Services.Models.Cars;
    using Services.Models.Parts;
    using System.Collections.Generic;

    public class PartsByCarModel
    {
        public IEnumerable<PartModel> Parts { get; set; }

        public CarModel Car { get; set; }
    }
}
