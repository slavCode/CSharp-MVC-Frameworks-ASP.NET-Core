namespace BookShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Category;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryServiceModel>> AllAsync()
            => await this.db
                         .Categories
                         .OrderByDescending(c => c.Id)
                         .ProjectTo<CategoryServiceModel>()
                         .ToListAsync();

        public async Task<CategoryServiceModel> ByIdAsync(int id)
            => await this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> EditAsync(int id, string name)
        {
            var exist = await this.db.Categories.AnyAsync(c => c.Name == name);
            if (exist) return false;

            var category = await this.db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            category.Name = name;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<int>> CreateMultipleAsync(string categoryNames)
        {
            if (string.IsNullOrEmpty(categoryNames)) return null;

            var categoryIds = new List<int>();
            foreach (var name in categoryNames.Split(' ').ToList())
            {
                var categoryFromDb = await this.db.Categories.FirstOrDefaultAsync(c => c.Name == name);
                if (categoryFromDb == null)
                {
                    var category = new Category { Name = name };

                    categoryIds.Add(category.Id);

                    await this.db.AddAsync(category);
                    await this.db.SaveChangesAsync();
                }
                else categoryIds.Add(categoryFromDb.Id);
            }

            return categoryIds;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return false;

            this.db.Remove(category);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
