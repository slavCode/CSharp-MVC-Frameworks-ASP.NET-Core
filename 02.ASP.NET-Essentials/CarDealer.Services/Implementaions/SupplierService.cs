namespace CarDealer.Services.Implementaions
{
    using Data;
    using Models.Enums;
    using Models.Suppliers;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All(SupplierType type)
        {
            var suppliers = db.Suppliers.AsQueryable();

            switch (type)
            {
                case SupplierType.Local:
                    suppliers = suppliers
                         .Where(s => !s.IsImporter);


                    break;
                case SupplierType.Importers:
                    suppliers = suppliers
                        .Where(s => s.IsImporter);

                    break;
            }

            return suppliers
                .Select(s => new SupplierModel()
                {
                    Name = s.Name,
                    Id = s.Id,
                    NumberOfParts = s.Parts.Count
                })
                .ToList();

        }
    }
}
