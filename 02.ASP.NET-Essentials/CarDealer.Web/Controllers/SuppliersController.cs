namespace CarDealer.Web.Controllers
{
    using Data;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
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
        private readonly Logger logger;

        private readonly string ControllerAsString = "suppliers";

        public SuppliersController(ISupplierService suppliers, ILogService logs)
        {
            this.suppliers = suppliers;
            this.logger = new Logger(logs);
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

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Create));
            }

            this.suppliers.Create(supplierModel.Name, supplierModel.IsImporter);
            this.logger.Create(this.User, nameof(Create), ControllerAsString);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            return View(this.suppliers.ById(id));
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Edit));
            }

            this.suppliers.Edit(supplierModel.Id, supplierModel.Name, supplierModel.IsImporter);
            this.logger.Create(this.User, nameof(Edit), ControllerAsString);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [Route(nameof(Delete) + "/{id}")]
        public IActionResult Delete(int id)
        {
            this.suppliers.Delete(id);
            this.logger.Create(this.User, nameof(Delete), ControllerAsString);

            return RedirectToAction(nameof(All));
        }

    }
}
