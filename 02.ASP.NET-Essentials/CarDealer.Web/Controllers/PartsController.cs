namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Parts;
    using Services;
    using System;
    using System.Linq;

    public class PartsController : Controller
    {
        private const int PageSize = 25;

        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult All(int page = 1)
        {
            return View(new PartPageListingModel
            {
                CurrentPage = page,
                Parts = this.parts.All(PageSize, page),
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)PageSize)
            });
        }

        public IActionResult Create()
        {
            return View(new PartFormModel
            {
                AllSuppliers = this.suppliers.All().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
            });
        }

        [HttpPost]
        public IActionResult Create(PartFormModel model)
        {
            var quantity = model.Quantity > 0 ? model.Quantity : 1;

            this.parts.Create(model.Name, model.Price, model.SupplierId, quantity);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
