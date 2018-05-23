namespace LearningSystem.Service.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Mapping;
    using Data.Models;
    using Models;
    using Models.Users;

    public interface IAdminUserService : IMapFrom<User>
    {
        Task<IEnumerable<UserListingServiceModel>> AllAsync();
    }
}
