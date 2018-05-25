namespace BookShop.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> AllAsync();

        Task<CategoryServiceModel> ByIdAsync(int id);

        Task<IEnumerable<int>> CreateMultipleAsync(string categoryNames);
    }
}
