namespace CarDealer.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cars;
    using Services;
    using Services.Implementaions;

    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(CarDealerDbContext db)
        {
            this.cars = new CarService(db);
        }

        [Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {
            var orderedCars = this.cars.ByMake(make);

            return this.View(new CarsByMakeModel()
            {
                Cars = orderedCars,
                Make = make
            });
        }

        [Route("cars/all")]
        public IActionResult All()
        {
            var allCars = this.cars.All();

            return this.View(new AllCarsModel
            {
                Cars = allCars
            });
        }
    }
}
