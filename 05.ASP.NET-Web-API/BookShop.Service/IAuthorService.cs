namespace BookShop.Service
{
    using Models.Author;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<AuthorServiceModel> ByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string firstName, string lastName);

        Task<int> CreateAsync(string firstName, string lastName);
    }
}
