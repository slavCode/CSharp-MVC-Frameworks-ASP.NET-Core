namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Service;
    using System.Threading.Tasks;
    using Models.Author;

    using static WebConstants;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authors;
        private readonly IBookService books;

        public AuthorsController(
            IAuthorService authors,
            IBookService books)
        {
            this.authors = authors;
            this.books = books;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.authors.ByIdAsync(id));

        [HttpGet(WithId + WithBooks)]
        public async Task<IActionResult> GetBooks(int id)
            => this.OkOrNotFound(await this.books.ByAuthorAsync(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]AuthorPostRequestModel model)
            => Ok(await this.authors.CreateAsync(model.FirstName, model.LastName));
    }
}
