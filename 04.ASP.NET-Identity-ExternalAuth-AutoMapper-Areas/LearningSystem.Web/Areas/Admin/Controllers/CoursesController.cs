namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Courses;
    using Service.Admin;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Controllers;

    public class CoursesController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminCourseService courses;

        public CoursesController(
            UserManager<User> userManager,
            IAdminCourseService courses)
        {
            this.userManager = userManager;
            this.courses = courses;
        }

        public async Task<IActionResult> Create()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebGlobulConstants.TrainerRole);
            var trainersListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id
                });

            return View(new CreateCourseFormModel
            {
                Trainers = trainersListItems,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(60)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseFormModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("Invalid course details!");

                return RedirectToAction(nameof(Create));
            }

            var trainerExists = userManager.FindByIdAsync(courseModel.TrainerId);

            if (trainerExists == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid user!");
            }

            await this.courses.CreateAsync(courseModel.Name, courseModel.Description,
                courseModel.EndDate, courseModel.StartDate, courseModel.TrainerId);

            this.TempData.AddSuccessMessage($"{courseModel.Name} course added successfully.");

            return RedirectToAction(nameof(HomeController.Index),
                                    "Home", routeValues: new { area = string.Empty });
        }
    }
}
