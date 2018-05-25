namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Category;
    using Service;
    using System.Threading.Tasks;

    using static WebConstants;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> All()
            => this.OkOrNotFound(await this.categories.AllAsync());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.categories.FindAsync(id));

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]CategoryRequestModel model)
        {
            var exists = await this.categories.FindAsync(model.Name);
            if (exists) return BadRequest(CategoryErrorMessage);

            return this.OkOrNotFound(await this.categories.EditAsync(id, model.Name));
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
            => this.OkOrBadRequest(await this.categories.DeleteAsync(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CategoryRequestModel model)
        {
            var exists = await this.categories.FindAsync(model.Name);
            if (exists) return BadRequest(CategoryErrorMessage);

            return this.OkOrNotFound(await this.categories.CreateAsync(model.Name));
        }

    }
}
