namespace CarDealer.Web.Models.Suppliers
{
    using Services.Models.Enums;
    using Services.Models.Suppliers;
    using System.Collections.Generic;

    public class AllSuppliersModel
    {
        public IEnumerable<SupplierModel> SuppliersByType { get; set; }

        public SupplierType Type { get; set; }
    }
}
