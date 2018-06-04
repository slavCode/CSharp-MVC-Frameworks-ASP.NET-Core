namespace News.Service
{
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Item;

    public interface INewsService
    {
        Task<IEnumerable<ItemServiceModel>> AllAsync();

        Task<ItemServiceModel> ByIdAsync(int id);

        Task<Item> CreateAsync(string authorId, string title, string content, DateTime publishDate);

        Task<int?> UpdateAsync(int id, string title, string content, DateTime publishDate);

        Task<bool> DeleteAsync(int id);

        Task<bool> IsLoggedInUserAuthorAsync(int newsId, string userId);
    }
}
