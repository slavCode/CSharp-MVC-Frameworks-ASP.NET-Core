namespace LearningSystem.Web.Models.Search
{
    using Courses;
    using Users;

    public class SearchViewModel
    {
        public string Term { get; set; }

        public bool IsAnyCourses { get; set; }

        public CoursesWithPagingModel CoursesInfo { get; set; }

        public bool IsAnyUsers { get; set; }

        public UsersWithPagingModel UsersInfo { get; set; }
    }
}
