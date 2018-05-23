namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Service.Blog;
    using Service.Html;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static Data.Common.DataModelValidationConstants;
    using static Infrastructure.WebGlobulConstants;

    [Area(BlogArea)]
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IBlogArticlesService articles;
        private readonly UserManager<User> userManager;
        private readonly IHtmlService html;

        public ArticlesController(
            IBlogArticlesService articles,
            UserManager<User> userManager,
            IHtmlService html)
        {
            this.articles = articles;
            this.userManager = userManager;
            this.html = html;
        }

        public async Task<IActionResult> Index(int page = 1, string searchTerm = "")
        {
            var model = new ArticlesListingViewModel();

            if (String.IsNullOrEmpty(searchTerm))
            {
                var resultArticles = await this.articles.AllAsync(page);

                model.Articles = resultArticles;
                model.TotalArticles = await this.articles.TotalAsync();
            }
            else
            {
                model.Articles = await this.articles.SearchAsync(page, searchTerm);
                model.TotalArticles = await this.articles.TotalFromSearchAsync(searchTerm);
                model.SearchTerm = searchTerm;

                if (!model.Articles.Any())
                {
                    this.TempData.AddErrorMessage($@"Sorry, no results found for ""{model.SearchTerm}"".");

                    return RedirectToAction(nameof(Index), new { page = 1, searchTerm = "" });
                }
            }

            model.CurrentPage = page;

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articles.ByIdAsync(id);

            return this.ViewOrNotFound(new ArticleDetailsViewModel
            {
                Title = article.Title,
                Content = article.Content,
                Author = article.Author,
                PublishDate = article.PublishDate
            });
        }

        [Authorize(Roles = AuthorRole)]
        public IActionResult Create()
           => View();

        [HttpPost]
        [Authorize(Roles = AuthorRole)]
        public async Task<IActionResult> Create(PublishArticleFormModel articleModel)
        {
            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage(
                    $@"Title must be between {ArticleTitleMinLength} and {ArticleTitleMaxLength} symbols. Content must be more then {ArticleContentMinLength} symbols.");

                return RedirectToAction(nameof(Create));
            }

            articleModel.Content = this.html.Sanitize(articleModel.Content);

            var userId = userManager.GetUserId(User);
            await this.articles.Create(articleModel.Title, articleModel.Content, userId);

            this.TempData.AddSuccessMessage($"{articleModel.Title} published successfuly.");

            return RedirectToAction(nameof(Index), new { page = 1, searchTerm = string.Empty });
        }
    }
}
