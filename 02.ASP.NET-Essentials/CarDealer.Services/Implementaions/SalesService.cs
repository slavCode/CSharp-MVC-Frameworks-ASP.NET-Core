namespace CarDealer.Services.Implementaions
{
    using Data;
    using Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SalesService : ISalesService
    {
        private readonly CarDealerDbContext db;

        public SalesService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleModel> All()
        {
            return db
                .Sales
                .Select(s => new SaleModel
                {
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
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CustomerName = s.Customer.Name,
                    TotalPrice = s.Car.Parts.Sum(p => p.Part.Price),
                    CarTravelledDistance = s.Car.TravelledDistance,
                    Discount = s.Discount,
                    DiscountedPrice = (double)(s.Car.Parts.Sum(p => p.Part.Price)) - s.Discount,
                });
        }

        // TODO 
        //private List<SaleModel> ListSales(List<Sale> sales)
        //{
        //    return sales
        //        .Select(s => new SaleModel
        //        {
        //            CarMake = s.Car.Make,
        //            CarModel = s.Car.Model,
        //            CustomerName = s.Customer.Name,
        //            TotalPrice = s.Car.Parts.Sum(p => p.Part.Price),
        //            CarTravelledDistance = s.Car.TravelledDistance,
        //            Discount = s.Discount,
        //            DiscountedPrice = (double)(s.Car.Parts.Sum(p => p.Part.Price)) - s.Discount,
        //        })
        //        .ToList();
        //}
    }
}
