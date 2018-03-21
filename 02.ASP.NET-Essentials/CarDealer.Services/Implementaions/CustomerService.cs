namespace CarDealer.Services.Implementaions
{
    using Data;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Customers;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customers = db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customers = customers
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);

                    break;
                case OrderDirection.Descending:
                    customers = customers
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);

                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction{order}.");
            }

            return customers.Select(c =>
                new CustomerModel
                {
                    BirthDate = c.BirthDate,
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerByIdModel CustomerById(int id)
        {
            return db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerByIdModel
                {
                    Name = c.Name,
                    TotalCars = c.Sales.Count,
                    MoneySpend = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price))
                })
                .FirstOrDefault();
        }
    }
}
