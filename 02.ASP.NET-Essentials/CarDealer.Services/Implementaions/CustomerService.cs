namespace CarDealer.Services.Implementaions
{
    using Data;
    using Data.Models;
    using Models.Customers;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                    Id = c.Id,
                    BirthDate = c.BirthDate,
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public void Edit(int id, string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = db
                .Customers
                .FirstOrDefault(c => c.Id == id);

            customer.Name = name;
            customer.BirthDate = birthday;
            customer.IsYoungDriver = isYoungDriver;

            db.SaveChanges();
        }

        public CustomerModel ById(int id)
        {
            return db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    TotalCars = c.Sales.Count,
                    MoneySpend = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price)),
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();
        }

        public void Create(string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthday,
                IsYoungDriver = isYoungDriver
            };

            this.db.Customers.Add(customer);
            db.SaveChanges();
        }
    }
}
