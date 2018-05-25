namespace BookShop.Service
{
    using Models.Book;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<BookWithAuthorServiceModel> ByIdAsync(int id);

        Task<IEnumerable<BookServiceModel>> ByAuthorAsync(int id);

        Task<IEnumerable<BookWithTitleOnlyServiceModel>> FindAsync(string term);

        Task<int?> EditAsync(int id, 
                             string title, 
                             string description, 
                             decimal price, 
                             int copies,
                             int edition, 
                             int? ageRestriction, 
                             DateTime releaseDate, 
                             int authorId);

        Task<bool> DeleteAsync(int id);

        Task<int?> CreateAsync(int authorId, 
                               string title, 
                               string description, 
                               decimal price,
                               int copies, 
                               int edition, 
                               int? ageRestriction, 
                               DateTime releaseDate,
                               IEnumerable<int> categoryIds);
    }
}
