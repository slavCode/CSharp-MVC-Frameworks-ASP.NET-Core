namespace CarDealer.Web.Controllers
{
    using Data;
    using Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Implementaions;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(CarDealerDbContext db)
        {
            this.cars = new CarService(db);
        }

        [Route("{make}")]
        public IActionResult ByMake(string make)
        {
            var orderedCars = this.cars.ByMake(make);

            return this.View(new CarsByMakeModel()
            {
                Cars = orderedCars,
                Make = make
            });
        }

        [Route(nameof(All))]
        public IActionResult All()
        {
            var carsWithParts = this.cars.WithParts();

            return this.View(carsWithParts);
        }
    }
}
