namespace CarDealer.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Cars;
    using Services;
    using Services.Implementaions;
    using System.Linq;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(CarDealerDbContext db)
        {
            this.cars = new CarService(db);
            this.parts = new PartService(db);
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

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View(new CarFormModel
                {
                    AllParts = this.parts.All().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    })
            });

        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            var partsIds = formModel.PartIds;

            this.cars.Create(formModel.Make, formModel.Model, formModel.TravelledDistance, partsIds);

            return RedirectToAction(nameof(All));
        }
    }
}
