namespace CarDealer.Web.Models.Cars
{
    using Services.Models.Cars;
    using System.Collections.Generic;

    public class AllCarsModel
    {
        public IEnumerable<CarModel> Cars { get; set; }
    }
}
