namespace BookShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models.Author;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Authors.AnyAsync(a => a.Id == id);

        public async Task<bool> ExistsAsync(string firstName, string lastName)
            => await this.db
                .Authors
                .AnyAsync(a => a.FirstName.ToLower() == firstName.ToLower() 
                            && a.LastName.ToLower() == lastName.ToLower());

        public async Task<int> CreateAsync(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName.Capitalize(),
                LastName = lastName.Capitalize()
            };

            await this.db.AddAsync(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }
    }
}
