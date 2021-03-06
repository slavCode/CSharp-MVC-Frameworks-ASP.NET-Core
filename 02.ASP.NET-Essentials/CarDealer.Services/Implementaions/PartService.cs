﻿namespace CarDealer.Services.Implementaions
{
    using Data;
    using Data.Models;
    using Models.Parts;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;


        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartListingModel> All(int pageSize, int page = 1)
        {
            return this.db
                .Parts
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Price = p.Price,
                    Name = p.Name,
                    SupplierName = p.Supplier.Name,
                    Quantity = p.Quantity
                })
                .ToList();
        }

        public int Total()
        {
            return this.db
                .Parts
                .Count();
        }

        public void Create(string name, decimal price, int supplierId, int quantity)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                SupplierId = supplierId
            };

            this.db.Add(part);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var part = this.db
                  .Parts
                  .FirstOrDefault(p => p.Id == id);

            this.db.Parts.Remove(part);
            this.db.SaveChanges();
        }

        public void Edit(int id, decimal price, int quntity)
        {
            var part = this.db
                .Parts
                .FirstOrDefault(p => p.Id == id);

            part.Price = price;
            part.Quantity = quntity;

            this.db.SaveChanges();
        }

        public PartListingModel ById(int id)
        {
            var test = this.db.Find<PartCars>(1, 20);


            return this.db
                .Parts
                .Where(p => p.Id == id)
                .Select(p => new PartListingModel()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name

                })
                .FirstOrDefault();
        }
    }
}
