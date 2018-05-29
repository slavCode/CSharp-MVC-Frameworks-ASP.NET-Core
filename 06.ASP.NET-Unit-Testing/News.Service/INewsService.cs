namespace News.Service
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INewsService
    {
        Task<IEnumerable<ItemServiceModel>> AllAsync();

        Task<int> CreateAsync(string title, string content, DateTime publishDate);

        Task<int?> UpdateAsync(int id, string title, string content, DateTime publishDate);

        Task<bool> DeleteAsync(int id);
    }
}
