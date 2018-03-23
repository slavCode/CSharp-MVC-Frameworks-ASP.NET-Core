namespace CarDealer.Services
{
    using Models.Enums;
    using Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierWithPartsModel> AllWithParts(SupplierType type);

        IEnumerable<SupplierModel> All();
    }
}
