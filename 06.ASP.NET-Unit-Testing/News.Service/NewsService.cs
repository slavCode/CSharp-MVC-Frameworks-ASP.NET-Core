namespace News.Service
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class NewsService : INewsService
    {
        private readonly NewsDbContext db;

        public NewsService(NewsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ItemServiceModel>> AllAsync()
            => await this.db
                      .Items
                      .OrderBy(i => i.PublishDate)
                      .ProjectTo<ItemServiceModel>()
                      .ToListAsync();

        public async Task<int>  CreateAsync(string title, string content, DateTime publishDate)
        {
            var item = new Item
            {
                Title = title,
                Content = content,
                PublishDate = publishDate
            };

            this.db.Add(item);
            await this.db.SaveChangesAsync();

            return item.Id;
        }

        public async Task<int?> UpdateAsync(int id, string title, string content, DateTime publishDate)
        {
            var item = await this.db.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return null;

            item.Title = title;
            item.Content = content;
            item.PublishDate = publishDate;

            await this.db.SaveChangesAsync();

            return item.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await this.db.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null) return false;

            this.db.Remove(item);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
