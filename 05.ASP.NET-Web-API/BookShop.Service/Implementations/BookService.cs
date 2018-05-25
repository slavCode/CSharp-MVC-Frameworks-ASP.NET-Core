namespace BookShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Infrastructure;
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

       public async Task<bool> TitleExistsAsync(string title)
            => await this.db.Books.AnyAsync(b => b.Title.ToLower() == title.ToLower());

        public async Task<BookWithAuthorServiceModel> ByIdAsync(int id)
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

        public async Task<int?> EditAsync(
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
            var book = await FindAsync(id);
            if (book == null) return null;

            book.Title = title.Capitalize();
            book.Description = description.Capitalize();
            book.Price = price;
            book.Copies = copies;
            book.Edition = edition;
            book.AgeRestriction = ageRestriction;
            book.ReleaseDate = releaseDate;
            book.AuthorId = authorId;

            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await FindAsync(id);
            if (book == null) return false;

            this.db.Books.Remove(book);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<int?> CreateAsync(
              int authorId,
              string title,
              string description,
              decimal price,
              int copies,
              int edition,
              int? ageRestriction,
              DateTime releaseDate,
              IEnumerable<int> categoryIds)
        {
            var book = new Book
            {
                AuthorId = authorId,
                Title = title.Capitalize(),
                Description = description.Capitalize(),
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate
            };

            var categoriesInBook = new List<CategoryBooks>();
            foreach (var categoryId in categoryIds)
            {
                var categoryBooks = new CategoryBooks { BookId = book.Id, CategoryId = categoryId };
                categoriesInBook.Add(categoryBooks);
            }

            book.Categories.AddRange(categoriesInBook);

            this.db.Add(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        private async Task<Book> FindAsync(int id)
            => await this.db.Books.FirstOrDefaultAsync(b => b.Id == id);
    }
}
