namespace BookShop.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Book;

    public interface IBookService
    {
        Task<BookWithAuthorServiceModel> ById(int id);

        Task<IEnumerable<BookServiceModel>> ByAuthorAsync(int id);

        Task<IEnumerable<BookWithTitleOnlyServiceModel>> FindAsync(string term);
    }
}
