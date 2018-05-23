namespace CameraBazaar.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models;
    using System.Security.Claims;

    public class CamerasController : Controller
    {
        private readonly ICameraService cameras;
        private readonly IMapper mapper;

        public CamerasController(ICameraService cameras, IMapper mapper)
        {
            this.cameras = cameras;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            return View(this.cameras.All());
        }

        public IActionResult Details(int id)
        {
            var camera = this.cameras.ById(id);

            return View(camera);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CameraAddModel addCameraModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Add));
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.cameras.Create(addCameraModel, userId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var camera = this.cameras.ById(id);

            if (camera == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(this.mapper.Map<CameraEditModel>(camera));

            //return View(new CameraAddViewModel
            //{
            //    Id = id,
            //    Make = camera.Make,
            //    Model = camera.Model,
            //    Price = camera.Price,
            //    Quantity = camera.Quantity,
            //    MinShutterSpeed = camera.MinShutterSpeed,
            //    MaxShutterSpeed = camera.MaxShutterSpeed,
            //    MinIso = camera.MinIso,
            //    MaxIso = camera.MaxIso,
            //    IsFullFrame = camera.IsFullFrame,
            //    VideoResulution = camera.VideoResulution,
            //    Description = camera.Description,
            //    ImageUrl = camera.ImageUrl,
            //    UserId = camera.UserId,
            //    Username = camera.Username
            //});
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CameraEditModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            this.cameras.Edit(cameraModel);

            //this.cameras.Edit(
            //    cameraModel.Id,
            //    cameraModel.Make,
            //    cameraModel.Model,
            //    cameraModel.Price,
            //    cameraModel.Quantity,
            //    cameraModel.MinShutterSpeed,
            //    cameraModel.MaxShutterSpeed,
            //    cameraModel.MinIso,
            //    cameraModel.MaxIso,
            //    cameraModel.IsFullFrame,
            //    cameraModel.VideoResulution,
            //    cameraModel.LightMeterings,
            //    cameraModel.Description,
            //    cameraModel.ImageUrl
            //    );

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            this.cameras.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
