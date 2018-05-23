namespace LearningSystem.Service.Blog
{
    using Core.Mapping;
    using Data.Models;
    using Models.Articles;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticlesService : IMapFrom<Article>
    {
        Task<int> TotalAsync();

        Task<int> TotalFromSearchAsync(string searchTerm);

        Task<ArticleDetailsServiceModel> ByIdAsync(int id);

        Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page);

        Task<IEnumerable<ArticleListingServiceModel>> SearchAsync(int page, string searchTerm);

        Task Create(string title, string content, string authorId);
    }
}
