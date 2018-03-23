namespace CarDealer.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services;
    using Services.Implementaions;
    using Services.Models.Enums;

    public class SuppliersController : Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(CarDealerDbContext db)
        {
            this.suppliers = new SupplierService(db);
        }

        [Route("suppliers/{type}")]
        public IActionResult All(string type)
        {
            var supplierType = type.ToLower() == "importers" 
                ? SupplierType.Importers 
                : SupplierType.Local;

           var suppliersByType =  this.suppliers.AllWithParts(supplierType);

            return View(new AllSuppliersModel()
            {
                SuppliersByType = suppliersByType,
                Type = supplierType
            });
        }
    }
}
