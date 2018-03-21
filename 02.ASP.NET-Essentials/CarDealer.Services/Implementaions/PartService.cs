namespace CarDealer.Services.Implementaions
{
    using Data;
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

        public IEnumerable<PartModel> ByCarId(int carId)
        {
            return db
                .Parts
                .Where(p => p.Cars.Any(c => c.CarId == carId))
                .Select(p => new PartModel
                {
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();
        }
    }
}
