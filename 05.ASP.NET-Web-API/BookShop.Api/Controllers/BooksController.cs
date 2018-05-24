namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Book;
    using Service;
    using System.Threading.Tasks;

    using static WebConstants;

    public class BooksController : BaseController
    {
        private readonly IBookService books;

        public BooksController(IBookService books)
        {
            this.books = books;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.books.ById(id));

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string search = "")
        {
            var result = await this.books.FindAsync(search);

            return this.OkOrNotFound(result);
        }

        [HttpPut(WithId)]
        public async Task<IActionResult> Put(int id, [FromBody]BookPutRequestModel model)
        {
            if (model == null) return BadRequest();

            var success = await this.books.Edit(id, model.Title, model.Description, model.Price,
                                              model.Copies, model.Edition, model.AgeRestriction,
                                              model.ReleaseDate, model.AuthorId);

            if (!success) return BadRequest();

            return Ok();
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await this.books.Delete(id);
            if (!success) return BadRequest();

            return Ok();
        }
    }
}
