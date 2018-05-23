namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Trainer;
    using Service;
    using Service.Models.Courses;
    using System;
    using System.Threading.Tasks;

    [Authorize(Roles = WebGlobulConstants.TrainerRole)]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainers;
        private readonly UserManager<User> userManager;
        private readonly ICourseService courses;

        public TrainerController(
            ITrainerService trainers,
            UserManager<User> userManager,
            ICourseService courses)
        {
            this.trainers = trainers;
            this.userManager = userManager;
            this.courses = courses;
        }

        public async Task<IActionResult> Courses()
        {
            var userId = this.userManager.GetUserId(User);

            return View(await this.trainers.CoursesAsync(userId));
        }

        public async Task<IActionResult> Students(int courseId, int page = 1)
        {
            var trainerId = this.userManager.GetUserId(User);
            if (!await this.trainers.IsTrainerInCourse(trainerId, courseId)) return BadRequest();

            var course = await this.courses.ByIdAsync<TrainerCourseListingServiceModel>(courseId);
            var students = await this.trainers.StudentsAsync(courseId, page);
            var totalStudentsInCourse = await this.trainers.TotalStudentsAsync(courseId);

            var model = new TrainerCourseWithStudentsViewModel
            {
                CurrentPage = page,
                TotalStudents = totalStudentsInCourse,
                Course = course,
                Students = students
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GradeStudent(
             string studentId, Grade grade, int courseId, string studentName)
        {
            var trainerId = this.userManager.GetUserId(User);
            if (!await this.trainers.IsTrainerInCourse(trainerId, courseId)) return BadRequest();

            if (String.IsNullOrEmpty(studentId)) return BadRequest();

            var success = await this.trainers.GradeStudentAsync(studentId, grade, courseId);
            if (!success) return BadRequest();

            this.TempData.AddSuccessMessage($"{studentName} grade is now {grade.ToString()}.");

            return RedirectToAction(nameof(Students), new { courseId });
        }

        public async Task<IActionResult> DownloadExam(string studentId, int courseId)
        {
            var userId = this.userManager.GetUserId(User);
            if (!await this.trainers.IsTrainerInCourse(userId, courseId)) return BadRequest();

            var exam = await this.trainers.DownloadExam(userId, courseId);
            if (exam == null)
            {
                this.TempData.AddErrorMessage("No student exam!");
                return RedirectToAction(nameof(Students), new {courseId, page = 1});
            }

            var examInfo = await this.trainers.GetNamesAsync(studentId, courseId);

            return File(exam, "application/zip",
                $"{DateTime.UtcNow.ToShortDateString().ToString()}" +
                $"-{examInfo.Username}" +
                $"-{examInfo.CourseName}.zip");
        }
    }
}
