namespace LearningSystem.Service.Models.Users
{
    using Core.Mapping;
    using Data.Models;

    public class UserInCourseListingModel : IMapFrom<User>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }
    }
}
