namespace LearningSystem.Web.Controllers
{
    using Data.Common;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Courses;
    using Service;
    using Service.Models.Courses;
    using System.Threading.Tasks;

    public class CoursesController : Controller
    {
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public CoursesController(
            ICourseService courses,
            UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new CourseDetailsViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.IsStudentInCourse = await this.courses.IsStudentInCourseAsync(userId, id);
            }

            model.Course = await this.courses.ByIdAsync<CourseDetailsServiceModel>(id);

            if (model.Course == null) return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Submission(int courseId, IFormFile exam)
        {
            if (exam == null) return BadRequest();

            if (exam.Length > DataModelValidationConstants.ExamSubmissionLength
                || !exam.FileName.EndsWith(".zip"))
            {
                this.TempData.AddErrorMessage(@"Invalid operation! File must be in ""zip"" format and less then 2 MB! ");

                return RedirectToAction(nameof(Details), new { id = courseId });
            }

            var studentId = this.userManager.GetUserId(User);

            var success = await this.courses.AddSubmissionAsync(studentId, courseId, exam.Read());
            if (!success)
            {
                this.TempData.AddErrorMessage("Invalid operation!");

                return RedirectToAction(nameof(Details), new { id = courseId });
            }

            this.TempData.AddSuccessMessage("File added successfully.");

            return RedirectToAction(nameof(Details), new { id = courseId });
        }

        public async Task<IActionResult> SignIn(int courseId)
        {
            var success = await this.courses
                                    .SignInStudentAsync(this.userManager.GetUserId(User), courseId);

            if (!success) return BadRequest();

            this.TempData.AddSuccessMessage($"You joined the course successfully :)");

            return RedirectToAction(nameof(Details), new { id = courseId });
        }

        public async Task<IActionResult> SignOut(int courseId)
        {
            var success = await this.courses.SignOutStudentAsync(this.userManager.GetUserId(User), courseId);

            if (!success) return BadRequest();

            this.TempData.AddErrorMessage($"You are signed out from the course. Sorry to hear that :( ");

            return RedirectToAction(nameof(Details), new { id = courseId });
        }
    }
}
