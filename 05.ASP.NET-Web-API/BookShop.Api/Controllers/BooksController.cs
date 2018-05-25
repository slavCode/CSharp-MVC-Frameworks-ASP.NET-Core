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

        public BooksController(IBookService books, ICategoryService categories)
        {
            this.books = books;
            this.categories = categories;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.books.ById(id));

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string search = "")
            => this.OkOrNotFound(await this.books.FindAsync(search));

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]BookPutRequestModel model)
        {
            var success = await this.books.Edit(id, model.Title, model.Description, model.Price,
                                              model.Copies, model.Edition, model.AgeRestriction,
                                              model.ReleaseDate, model.AuthorId);

            return this.OkOrBadRequest(success);
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
            => this.OkOrBadRequest(await this.books.Delete(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] BookPostRequestModel model)
        {
            var categoryIds = await this.categories.CreateMultipleAsync(model.Categories);

            var succeess = await this.books.Create(model.AuthorId, model.Title, model.Description,
                                             model.Price, model.Copies,model.Edition, model.AgeRestriction,
                                             model.ReleaseDate, categoryIds);
            return this.OkOrBadRequest(succeess);
        }
    }
}
