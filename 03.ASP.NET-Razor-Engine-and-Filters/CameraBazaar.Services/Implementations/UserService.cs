namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserDetailsModel ById(string id)
            => this.db
                   .Users
                   .Where(u => u.Id == id)
                   .Select(u => new UserDetailsModel
                   {
                       Id = u.Id,
                       Username = u.UserName,
                       Email = u.Email,
                       InStockCameras = u.Cameras.Count(c => c.Quantity > 0),
                       OutOfStockCameras = u.Cameras.Count(c => c.Quantity == 0),
                       Cameras = u.Cameras.Select(c => new CameraListingModel
                       {
                           Id = c.Id,
                           Model = c.Model,
                           InStock = c.Quantity > 0 ? true : false,
                           Make = c.Make,
                           Price = c.Price,
                           Image = c.ImageUrl
                       }),
                       Phone = u.PhoneNumber,
                       LastLoginTime = u.LastLoginTime
                   })
                   .FirstOrDefault();

        public IEnumerable<UserModel> All()
            => this.db
                .Users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Username = u.UserName
                })
                .ToList();

        public void AddLoginTime(string username)
        {
            var user = this.db
                .Users
                .FirstOrDefault(u => u.UserName == username);

            user.LastLoginTime = DateTime.UtcNow;

            this.db.SaveChanges();
        }
    }
}
