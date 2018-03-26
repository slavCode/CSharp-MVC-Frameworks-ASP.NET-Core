namespace CarDealer.Services
{
    using Models.Enums;
    using Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierWithPartsModel> AllWithParts(SupplierType type);

        IEnumerable<SupplierModel> All();

        void Create(string name, bool isImporter);

        void Edit(int id, string name, bool isImporter);

        void Delete(int id);

        SupplierModel ById(int id);
    }
}
