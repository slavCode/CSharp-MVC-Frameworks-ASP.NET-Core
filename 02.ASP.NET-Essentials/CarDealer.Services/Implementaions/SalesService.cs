namespace CarDealer.Services.Implementaions
{
    using Data;
    using Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;

    public class SalesService : ISalesService
    {
        private readonly CarDealerDbContext db;

        public SalesService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public SaleReviewModel SaleReview(int carId, int customerId, double discount)
        {
            var car = this.db
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new
                {
                    Name = $"{c.Make} {c.Model}",
                    Price = c.Parts.Sum(cp => cp.Part.Price)
                })
                .FirstOrDefault();

            var customer = this.db
                .Customers
                .FirstOrDefault(cu => cu.Id == customerId);

            var finalDiscount = !customer.IsYoungDriver
                ? discount
                : discount + 5;

            var discountAsString = !customer.IsYoungDriver
                ? $"{discount}%"
                : $"{discount}% (+5%)";

            return new SaleReviewModel
            {
                Price = car.Price,
                DiscountedPrice = car.Price - (car.Price / 100) * (decimal)finalDiscount,
                CarId = carId,
                Car = car.Name,
                CustomerId = customerId,
                Customer = customer.Name,
                DiscountAsString = discountAsString
            };
        }

        public void Create(int carId, int customerId, double discount)
        {
            var sale = new Sale
            {
                CarId = carId,
                CustomerId = customerId,
                Discount = discount
            };

            this.db.Add(sale);
            this.db.SaveChanges();
        }

        public IEnumerable<SaleModel> All()
        {
            return db
                .Sales
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CustomerName = s.Customer.Name,
                    TotalPrice = s.Car.Parts.Sum(p => p.Part.Price),
                    CarTravelledDistance = s.Car.TravelledDistance,
                    Discount = s.Discount,
                    DiscountedPrice = (double)(s.Car.Parts.Sum(p => p.Part.Price)) - s.Discount,
                });
        }

        public SaleModel ById(int id)
        {
            return db
                .Sales
                .Where(s => s.Id == id)
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CustomerName = s.Customer.Name,
                    TotalPrice = s.Car.Parts.Sum(p => p.Part.Price),
                    CarTravelledDistance = s.Car.TravelledDistance,
                    Discount = s.Discount,
                    DiscountedPrice = (double)(s.Car.Parts.Sum(p => p.Part.Price)) - s.Discount,
                })
                .FirstOrDefault();
        }

        public IEnumerable<SaleModel> Discounted()
        {
            return db
                .Sales
                .Where(s => s.Discount > 0.0)
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CustomerName = s.Customer.Name,
                    TotalPrice = s.Car.Parts.Sum(p => p.Part.Price),
                    CarTravelledDistance = s.Car.TravelledDistance,
                    Discount = s.Discount,
                    DiscountedPrice = (double)(s.Car.Parts.Sum(p => p.Part.Price)) - s.Discount,
                });
        }

        public IEnumerable<SaleModel> DiscountedByPercent(double percent)
        {
            return db
                .Sales
                .Where(s => Math.Abs(s.Discount - percent) < 0.1)
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CustomerName = s.Customer.Name,
                    TotalPrice = s.Car.Parts.Sum(p => p.Part.Price),
                    CarTravelledDistance = s.Car.TravelledDistance,
                    Discount = s.Discount,
                    DiscountedPrice = (double)(s.Car.Parts.Sum(p => p.Part.Price)) - s.Discount,
                });
        }
    }
}
