namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Book;
    using Service;
    using System.Threading.Tasks;

    using static WebConstants;

    public class BooksController : BaseController
    {
        private readonly IBookService books;
        private readonly ICategoryService categories;
        private readonly IAuthorService authors;

        public BooksController(
            IBookService books,
            ICategoryService categories,
            IAuthorService authors)
        {
            this.books = books;
            this.categories = categories;
            this.authors = authors;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.books.ByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string search = "")
            => this.OkOrNotFound(await this.books.FindAsync(search));

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]BookEditRequestModel model)
            => this.OkOrNotFound(await this.books.EditAsync(
                        id,
                        model.Title,
                        model.Description,
                        model.Price,
                        model.Copies,
                        model.Edition,
                        model.AgeRestriction,
                        model.ReleaseDate,
                        model.AuthorId));

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
            => this.OkOrBadRequest(await this.books.DeleteAsync(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] BookCreateRequestModel model)
        {
            var authorExists = await this.authors.ExistsAsync(model.AuthorId);
            if (!authorExists) return BadRequest("Author does not exists.");

            var categoryIds = await this.categories.CreateMultipleAsync(model.Categories);

            var bookId = await this.books.CreateAsync(
                model.AuthorId,
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                categoryIds);

            return this.OkOrNotFound(bookId);
        }
    }
}
