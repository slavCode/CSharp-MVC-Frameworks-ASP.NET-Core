namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Users;
    using Service.Admin;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdminUserService users,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var resultUsers = this.users.AllAsync();
            var roles = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new AdminUsersListingViewModel
            {
                Roles = await roles,
                Users = await resultUsers
            });
        }

        public async Task<IActionResult> AddToRole(AddUserInRoleFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var roleExist = await this.roleManager.RoleExistsAsync(model.RoleName);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExist = user != null;

            if (!userExist || !roleExist)
            {
                ModelState.AddModelError(string.Empty, "Invalid Identity details!");
            }

            if (await userManager.IsInRoleAsync(user, model.RoleName))
            {
                this.TempData.AddErrorMessage($"{user.Name} is already {model.RoleName}.");

                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.RoleName);

            this.TempData.AddSuccessMessage($"{user.Name} is now {model.RoleName}.");

            return RedirectToAction(nameof(Index));
        }
    }
}
