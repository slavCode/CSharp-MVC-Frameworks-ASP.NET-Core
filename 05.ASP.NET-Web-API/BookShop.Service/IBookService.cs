namespace BookShop.Service
{
    using Models.Book;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<BookWithAuthorServiceModel> ById(int id);

        Task<IEnumerable<BookServiceModel>> ByAuthorAsync(int id);

        Task<IEnumerable<BookWithTitleOnlyServiceModel>> FindAsync(string term);

        Task<bool> Edit(int id, string title, string description, decimal price, int copies,
                        int edition, int? ageRestriction, DateTime releaseDate, int authorId);

        Task<bool> Delete(int id);

        Task<bool> Create(int authorId, string title, string description, decimal price, 
                          int copies, int edition, int? ageRestriction, DateTime releaseDate,
                          IEnumerable<int> categoryIds);
    }
}
