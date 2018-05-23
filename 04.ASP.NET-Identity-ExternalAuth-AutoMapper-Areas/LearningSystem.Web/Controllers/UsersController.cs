namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Service;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUserService users,
            UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            var profile = await this.users.ByIdAsync(user.Id);
            if (profile == null) return BadRequest();

            return View(profile);
        }

        [Authorize]
        public async Task<IActionResult> Certificate(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var certificate = await this.users.GetCertificate(courseId, userId);
            if (certificate == null) return BadRequest();

            return File(certificate, "application/pdf");
        }
    }
}
