namespace News.Test.Services
{
    using Data;
    using Data.Models;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Service;
    using System; 
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class NewsServiceTest
    {
        private const int Id = 1;
        private const string Title = "First";
        private const string Content = "First Content";
        public static readonly DateTime PublishDate = DateTime.UtcNow;

        public NewsServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnAllItemsWithOrder()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstNews = this.CreateItem();
            var secondNews = this.CreateItem(2, "Second", "Second Content", new DateTime(2018, 06, 06));
            var thirdNews = this.CreateItem(3, "Third", "Third Content", new DateTime(2018, 07, 07));

            await db.AddRangeAsync(firstNews, secondNews, thirdNews);
            await db.SaveChangesAsync();

            var service = new NewsService(db);

            // Act
            var result = await service.AllAsync();

            // Assert
            result.Should().Match(r => r.ElementAt(0).Id == 1).And.HaveCount(3);
        }

        [Fact]
        public async Task CreateAsyncShouldReturnCorrectId()
        {
            // Arrange
            var db = this.GetDatabase();

            var service = new NewsService(db);
            // Act
            var result = await service.CreateAsync(Title, Content, DateTime.UtcNow);
            var savedItem = db.Items.FirstOrDefaultAsync(i =>
                                            i.Title == Title
                                            && i.Content == Title
                                            && i.PublishDate == PublishDate);

            // Assert
            result.Should().Be(savedItem.Id);
        }

        [Fact]
        public async Task UpdateAsyncShouldReturnCorrectIdAndCorrectItemData()
        {
            // Arrange
            var item = this.CreateItem();

            var db = this.GetDatabase();
            await db.AddAsync(item);
            await db.SaveChangesAsync();

            var service = new NewsService(db);

            var newTitle = "New Title";
            var newContent = "New Content";
            var newPublishDate = PublishDate.AddDays(1);

            // Act
            var result = service.UpdateAsync(item.Id, newTitle, newContent, newPublishDate);

            // Assert
            result.Should().Be(item.Id);
            item.Content.Should().Be(newContent);
            item.Title.Should().Be(newTitle);
            item.PublishDate.Should().Be(newPublishDate);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnCorrectIdAndCorrectItemData()
        {
            // Arrange
            var item = this.CreateItem();

            var db = this.GetDatabase();
            await db.AddAsync(item);
            await db.SaveChangesAsync();

            var service = new NewsService(db);

            // Act
            var result = await service.DeleteAsync(item.Id);

            // Assert
            result.Should().Be(true);
            db.Items.Any(i => i.Id == item.Id).Should().Be(false);
        }

        private Item CreateItem(int id, string title, string content, DateTime publishDate)
            => new Item
            {
                Id = id,
                Title = title,
                Content = content,
                PublishDate = publishDate
            };

        private Item CreateItem()
            => new Item
            {
                Id = Id,
                Title = Title,
                Content = Content,
                PublishDate = PublishDate
            };

        private NewsDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<NewsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NewsDbContext(dbOptions);
        }
    }
}
