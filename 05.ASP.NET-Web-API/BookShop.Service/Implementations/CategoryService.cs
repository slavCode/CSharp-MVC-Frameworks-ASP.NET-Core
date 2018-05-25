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
    using Infrastructure;

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

        public async Task<CategoryServiceModel> FindAsync(int id)
            => await this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> ExistsAsync(string name)
            => await this.db
                .Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<bool> ExistsAsync(int id)
            => await this.db
                .Categories
                .AnyAsync(c => c.Id == id);

        public async Task<int?> CreateAsync(string name)
        {
            var category = new Category { Name = name.Capitalize() };

            this.db.Add(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<IEnumerable<int>> CreateMultipleAsync(string categoryNames)
        {
            if (string.IsNullOrEmpty(categoryNames)) return null;

            var categoryIds = new List<int>();
            foreach (var name in categoryNames.Split(' ').ToList())
            {
                var categoryFromDb = await this.db.Categories
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
                if (categoryFromDb == null)
                {
                    var category = new Category { Name = name.Capitalize() };

                    categoryIds.Add(category.Id);

                    await this.db.AddAsync(category);
                    await this.db.SaveChangesAsync();
                }
                else if (!categoryIds.Contains(categoryFromDb.Id))
                {
                    categoryIds.Add(categoryFromDb.Id);
                }
            }

            return categoryIds;
        }

        public async Task<int?> EditAsync(int id, string name)
        {
            var category = await this.FindByIdAsync(id);
            category.Name = name.Capitalize();

            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await this.FindByIdAsync(id);
            if (category == null) return false;

            this.db.Remove(category);
            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<Category> FindByIdAsync(int id)
            => await this.db.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}
