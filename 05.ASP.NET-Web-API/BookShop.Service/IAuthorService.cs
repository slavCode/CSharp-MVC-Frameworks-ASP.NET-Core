namespace BookShop.Service
{
    using System.Threading.Tasks;
    using Models.Author;

    public interface IAuthorService
    {
        Task<AuthorServiceModel> ByIdAsync(int id);

        Task<int> CreateAsync(string firstName, string lastName);
    }
}
