namespace BookShop.Api.Controllers
{
    using System.Linq;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Author;
    using Service;
    using System.Threading.Tasks;

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
        {
            var result = await this.books.ByAuthorAsync(id);
            if (!result.Any()) return NoContent();

            return this.OkOrNotFound(result);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]AuthorPostRequestModel model)
        {
            var exists = await this.authors.ExistsAsync(model.FirstName, model.LastName);
            if (exists) return BadRequest("Author already exists.");

            return this.OkOrNotFound(await this.authors.CreateAsync(model.FirstName, model.LastName));
        }
    }
}
