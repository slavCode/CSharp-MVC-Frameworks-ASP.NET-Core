namespace CarDealer.Services.Implementaions
{
    using Data;
    using Models.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> All()
        {
            return this.db
                .Cars
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();
        }

        public IEnumerable<CarModel> ByMake(string make)
        {
            var cars = db.Cars.AsQueryable();

            var orderedCars = cars
                 .Where(c => c.Make.ToLower() == make.ToLower())
                 .OrderBy(c => c.Model)
                 .ThenByDescending(c => c.TravelledDistance)
                 .Select(c => new CarModel
                 {
                     Model = c.Model,
                     Make = c.Make,
                     TravelledDistance = c.TravelledDistance
                 });

            return orderedCars;
        }

        public CarModel ByCarId(int carId)
        {
            return db
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarModel
                {
                    Model = c.Model,
                    Make = c.Make,
                    TravelledDistance = c.TravelledDistance
                })
                .FirstOrDefault();
        }
    }
}
