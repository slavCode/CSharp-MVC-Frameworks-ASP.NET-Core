namespace BookShop.Service
{
    using Models.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> AllAsync();

        Task<CategoryServiceModel> ByIdAsync(int id);

        Task<bool> EditAsync(int id, string name);

        Task<bool> CreateAsync(string name);

        Task<IEnumerable<int>> CreateMultipleAsync(string categoryNames);

        Task<bool> DeleteAsync(int id);
    }
}
