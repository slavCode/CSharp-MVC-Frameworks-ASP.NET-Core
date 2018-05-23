namespace LearningSystem.Service.Models.Users
{
    using Core.Mapping;
    using Data.Models;

    public class UserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
