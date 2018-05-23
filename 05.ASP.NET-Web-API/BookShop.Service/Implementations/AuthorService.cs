namespace BookShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Author;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<AuthorServiceModel> ByIdAsync(int id)
            => await this.db
                .Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            await this.db.AddAsync(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }
    }
}
