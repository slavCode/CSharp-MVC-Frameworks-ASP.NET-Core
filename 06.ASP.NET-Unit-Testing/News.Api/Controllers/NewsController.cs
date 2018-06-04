namespace News.Api.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Service;
    using Service.Models.Item;
    using System.Threading.Tasks;

    using static Infrastructure.WebConstants;

    [Authorize]
    [Route(ApiNews)]
    public class NewsController : Controller
    {
        private readonly INewsService news;
        private readonly UserManager<User> users;

        public NewsController(INewsService news, UserManager<User> users)
        {
            this.news = news;
            this.users = users;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
            => this.OkOrBadRequest(await this.news.AllAsync());

        [AllowAnonymous]
        [HttpGet(WithId)]
        public async Task<IActionResult> GetSingle([FromRoute]int id)
            => this.OkOrNotFound(await this.news.ByIdAsync(id));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = users.GetUserId(User);

            var authenticated = User.Identity.IsAuthenticated;
            if (!authenticated) return Unauthorized();

            var savedItem = await this.news
                .CreateAsync(userId, model.Title, model.Content, model.PublishDate);

            var item = ProjectToItemServiceModel(savedItem);

            return CreatedAtAction(nameof(GetSingle), new { id = savedItem.Id }, item);
        }

        [HttpPut(WithId)]
        public async Task<IActionResult> Put(int id, [FromBody] ItemRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = this.users.GetUserId(User);
            if (!await this.news.IsLoggedInUserAuthorAsync(id, userId)) return BadRequest();

            var updatedId = await this.news.UpdateAsync(id, model.Title, model.Content, model.PublishDate);

            return this.OkOrBadRequest(updatedId);
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.users.GetUserId(User);
            if (!await this.news.IsLoggedInUserAuthorAsync(id, userId)) return BadRequest();

            var success = await this.news.DeleteAsync(id);
            if (!success) return BadRequest();

            return Ok();
        }

        private static ItemServiceModel ProjectToItemServiceModel(Item savedItem)
        {
            return new ItemServiceModel
            {
                Title = savedItem.Title,
                Content = savedItem.Content,
                PublishDate = savedItem.PublishDate
            };
        }

      
    }
}
