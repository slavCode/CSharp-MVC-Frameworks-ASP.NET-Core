namespace BookShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Book;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<BookWithAuthorServiceModel> ById(int id)
            => await this.db
                .Books
                .Where(b => b.Id == id)
                .ProjectTo<BookWithAuthorServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<BookServiceModel>> ByAuthorAsync(int id)
            => await this.db
                .Books
                .Where(b => b.AuthorId == id)
                .ProjectTo<BookServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<BookWithTitleOnlyServiceModel>> FindAsync(string term)
            => await this.db
                .Books
                .Where(b => b.Title.ToLower().Contains(term.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .ProjectTo<BookWithTitleOnlyServiceModel>()
                .ToListAsync();

        public async Task<bool> Edit(
            int id,
            string title,
            string description,
            decimal price,
            int copies,
            int edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId)
        {
            var book = await this.db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return false;

            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.Edition = edition;
            book.AgeRestriction = ageRestriction;
            book.ReleaseDate = releaseDate;
            book.AuthorId = authorId;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var book = await this.db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return false;

            this.db.Books.Remove(book);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
