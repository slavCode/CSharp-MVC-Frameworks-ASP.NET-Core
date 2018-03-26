namespace CarDealer.Services.Implementaions
{
    using Data;
    using Data.Models;
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

        public IEnumerable<SupplierWithPartsModel> AllWithParts(SupplierType type)
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
                .Select(s => new SupplierWithPartsModel()
                {
                    Name = s.Name,
                    Id = s.Id,
                    NumberOfParts = s.Parts.Count
                })
                .ToList();

        }

        public IEnumerable<SupplierModel> All()
        {
            return this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    IsImporter = s.IsImporter,
                    Name = s.Name,
                    Id = s.Id
                })
                .ToList();
        }

        public void Create(string name, bool isImporter)
        {
            var supplier = new Supplier
            {
                Name = name,
                IsImporter = isImporter
            };

            this.db.Add(supplier);
            this.db.SaveChanges();
        }

        public void Edit(int id, string name, bool isImporter)
        {
            var supplier = this.db
                .Suppliers
                .FirstOrDefault(s => s.Id == id);

            supplier.Name = name;
            supplier.IsImporter = isImporter;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplier = this.db
                .Suppliers
                .FirstOrDefault(s => s.Id == id);

            this.db.Remove(supplier);
            this.db.SaveChanges();
        }

        public SupplierModel ById(int id)
        {
            return this.db
                .Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierModel
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .FirstOrDefault();
        }
    }
}
