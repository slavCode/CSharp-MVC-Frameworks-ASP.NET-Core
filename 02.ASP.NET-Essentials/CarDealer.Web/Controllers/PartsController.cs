namespace CarDealer.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.Parts;
    using Services;
    using Services.Implementaions;

    public class PartsController : Controller
    {
        private readonly IPartService parts;
        private readonly ICarService cars;

        public PartsController(CarDealerDbContext db)
        {
            this.parts = new PartService(db);
            this.cars = new CarService(db);
        }

        [Route("cars/{id}/parts")]
        public IActionResult Parts(string id)
        {
            var cardId = int.Parse(id);

            var partsByCarId = this.parts.ByCarId(cardId);
            var carByCarId = this.cars.ByCarId(cardId);

            return this.View(new PartsByCarModel
            {
                Parts = partsByCarId,
                Car = carByCarId
            });

        }
    }
}
