namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services;
    using Services.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        [TempData]
        public string StatusMessage { get; set; }

        public UsersController(
            IUserService users,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        [Route("all")]
        public IActionResult All()
        {
            return View(this.users.All());
        }

        [Authorize]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var logedInUser = await userManager.GetUserAsync(User);

            var user = this.users.ById(id);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            return View(new UserDetailsViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                Username = user.Username,
                Cameras = user.Cameras.Select(c => new CameraListingModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Image = c.Image,
                    InStock = c.InStock,
                    Price = c.Price
                }),
                InStockCameras = user.InStockCameras,
                OutOfStockCameras = user.OutOfStockCameras,
                IsLogedInUser = user.Id == logedInUser.Id,
                LastLoginTime = user.LastLoginTime
            });
        }

        [Authorize]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var model = new EditUserFormModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                LastLoginTime = user.LastLoginTime
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(EditUserFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await signInManager.SignInAsync(user, isPersistent: false);

            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(Details), new { user.Id });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
