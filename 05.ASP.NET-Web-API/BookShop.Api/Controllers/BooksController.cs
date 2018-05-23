namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
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
    }
}
