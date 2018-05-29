namespace News.Api.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Service;
    using System.Threading.Tasks;

    using static Infrastructure.WebConstants;

    public class NewsController : Controller
    {
        private readonly INewsService news;

        public NewsController(INewsService news)
        {
            this.news = news;
        }

        [HttpGet(ApiNews)]
        public async Task<IActionResult> Get()
            => Ok(await this.news.AllAsync());

        [HttpPost(ApiNews)]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] ItemRequestModel model)
            => Ok(await this.news.CreateAsync(model.Title, model.Content, model.PublishDate));

        [HttpPut(ApiNewsId)]
        [ValidateModel]
        public async Task<IActionResult> Put(int id, [FromBody] ItemRequestModel model)
        {
            var updatedId = await this.news.UpdateAsync(id, model.Title, model.Content, model.PublishDate);

            return this.OkOrNotFound(updatedId);
        }

        [HttpDelete(ApiNewsId)]
        public async Task<IActionResult> Delete(int id)
            => this.OkOrNotFound(await this.news.DeleteAsync(id));
    }
}
