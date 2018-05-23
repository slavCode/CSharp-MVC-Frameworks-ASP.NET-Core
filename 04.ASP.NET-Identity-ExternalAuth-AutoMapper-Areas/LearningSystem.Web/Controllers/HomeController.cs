namespace LearningSystem.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Courses;
    using Models.Search;
    using Models.Users;
    using Service;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService courses;
        private readonly IUserService users;

        public HomeController(ICourseService courses, IUserService users)
        {
            this.courses = courses;
            this.users = users;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new HomeIndexViewModel
            {
                CurrentPage = page,
                TotalCourses = await this.courses.TotalAsync(),
                Courses = await this.courses.AllAsync(page)
            });

        public async Task<IActionResult> Search(string term, bool inCourses, bool inUsers,
            int coursesPage = 1, int usersPage = 1)
        {
            if (!inUsers && !inCourses)
            {
                this.TempData.AddErrorMessage("Please select an option from the search bar.");

                return RedirectToAction(nameof(Index));
            }
            if (String.IsNullOrEmpty(term))
            {
                this.TempData.AddErrorMessage("Invalid search term.");

                return RedirectToAction(nameof(Index));
            }

            var model = new SearchViewModel();

            if (inCourses)
            {
                model.CoursesInfo = new CoursesWithPagingModel
                {
                    Courses = await this.courses.FindByNameAsync(term, coursesPage),
                    CurrentPage = coursesPage,
                    TotalCourses = await this.courses.TotalByNameAsync(term)
                };

                model.IsAnyCourses = model.CoursesInfo.TotalCourses > 0;
            }
            if (inUsers)
            {
                model.UsersInfo = new UsersWithPagingModel
                {
                    Users = await this.users.FindByNameAsync(term, usersPage),
                    CurrentPage = usersPage,
                    TotalUsers = await this.users.TotalByNameAsync(term)
                };

                model.IsAnyUsers = model.UsersInfo.TotalUsers > 0;
            }

            model.Term = term;

            return View(model);
        }

        public IActionResult Error()
           => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
