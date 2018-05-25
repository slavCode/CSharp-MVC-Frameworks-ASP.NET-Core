namespace BookShop.Service
{
    using Models.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> AllAsync();

        Task<CategoryServiceModel> FindAsync(int id);

        Task<bool> FindAsync(string name);

        Task<int?> CreateAsync(string name);

        Task<IEnumerable<int>> CreateMultipleAsync(string categoryNames);

        Task<int?> EditAsync(int id, string name);

        Task<bool> DeleteAsync(int id);
    }
}
