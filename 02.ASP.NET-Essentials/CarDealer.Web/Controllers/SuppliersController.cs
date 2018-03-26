namespace CarDealer.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services;
    using Services.Implementaions;
    using Services.Models.Enums;
    using Services.Models.Suppliers;

    [Route("suppliers")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(CarDealerDbContext db)
        {
            this.suppliers = new SupplierService(db);
        }

        [Route(nameof(All))]
        public IActionResult All()
        {
            return View(this.suppliers.All());

        }

        [Route("{type}")]
        public IActionResult AllByType(string type)
        {
            var supplierType = type.ToLower() == "importers"
                ? SupplierType.Importers
                : SupplierType.Local;

            var suppliersByType = this.suppliers.AllWithParts(supplierType);

            return View(new AllSuppliersModel()
            {
                SuppliersByType = suppliersByType,
                Type = supplierType
            });
        }

        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Create));
            }

            this.suppliers.Create(supplierModel.Name, supplierModel.IsImporter);

            return RedirectToAction(nameof(All));
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            return View(this.suppliers.ById(id));
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Edit));
            }

            this.suppliers.Edit(supplierModel.Id, supplierModel.Name, supplierModel.IsImporter);

            return RedirectToAction(nameof(All));
        }

        [Route(nameof(Delete) + "/{id}")]
        public IActionResult Delete(int id)
        {
            this.suppliers.Delete(id);

            return RedirectToAction(nameof(All));
        }

    }
}
