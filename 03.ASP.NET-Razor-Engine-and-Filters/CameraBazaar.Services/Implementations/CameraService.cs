namespace CameraBazaar.Services.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;
        private readonly IMapper mapper;

        public CameraService(CameraBazaarDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<CameraListingModel> All()
           => this.db
               .Cameras
               .Select(c => new CameraListingModel
               {
                   Id = c.Id,
                   Make = c.Make,
                   Model = c.Model,
                   Price = c.Price,
                   Image = c.ImageUrl,
                   InStock = true && c.Quantity > 0
               })
               .ToList();

        public CameraDetailsModel ById(int id)
        {
            if (!this.Exists(id))
            {
                return null;
            }

            return this.db
                .Cameras
                .Where(c => c.Id == id)
                .ProjectTo<CameraDetailsModel>()
                .FirstOrDefault();
        }

        public void Create(CameraAddModel addCameraModel, string userId)
        {
            var camera = this.mapper.Map<Camera>(addCameraModel);
            camera.UserId = userId;

            this.db.Add(camera);
            this.db.SaveChanges();
        }

        public void Edit(CameraEditModel cameraModel)
        {
            var camera = this.db.Cameras.FirstOrDefault(c => c.Id == cameraModel.Id);

            if (camera == null)
            {
                throw new Exception("Invalid camera.");
            }

            LightMetering? lightMetering = null;

            if (true && cameraModel.LightMeterings != null)
            {
                lightMetering = (LightMetering)cameraModel.LightMeterings.Cast<int>().Sum();
            }

            camera.Make = cameraModel.Make;
            camera.Model = cameraModel.Model;
            camera.Price = cameraModel.Price;
            camera.Quantity = cameraModel.Quantity;
            camera.MinShutterSpeed = cameraModel.MinShutterSpeed;
            camera.MaxShutterSpeed = cameraModel.MaxShutterSpeed;
            camera.MinIso = cameraModel.MinIso;
            camera.MaxIso = cameraModel.MaxIso;
            camera.IsFullFrame = cameraModel.IsFullFrame;
            camera.VideoResulution = cameraModel.VideoResulution;
            camera.LightMetering = lightMetering;
            camera.Description = cameraModel.Description;
            camera.ImageUrl = cameraModel.ImageUrl;

            this.db.SaveChanges();

        }

        public void Delete(int id)
        {
            var camera = this.db
                .Cameras
                .FirstOrDefault(c => c.Id == id);

            this.db.Remove(camera);
            this.db.SaveChanges();
        }

        private bool Exists(int id)
            => this.db.Cameras.Any(c => c.Id == id);
    }
}
