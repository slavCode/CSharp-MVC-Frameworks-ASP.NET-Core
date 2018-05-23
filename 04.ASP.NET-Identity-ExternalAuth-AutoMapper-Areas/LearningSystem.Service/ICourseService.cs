namespace LearningSystem.Service
{
    using Core.Mapping;
    using Data.Models;
    using Models.Courses;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService : IMapFrom<Course>
    {
        Task<int> TotalAsync();

        Task<TModel> ByIdAsync<TModel>(int id);

        Task<bool> IsStudentInCourseAsync(string studentId, int courseId);

        Task<IEnumerable<CourseListingServiceModel>> AllAsync(int page);

        Task<bool> SignInStudentAsync(string studentId, int courseId);

        Task<bool> SignOutStudentAsync(string studentId, int courseId);

        Task<IEnumerable<CourseListingServiceModel>> FindByNameAsync(string term, int page);

        Task<int> TotalByNameAsync(string term);

        Task<bool> AddSubmissionAsync(string studentId, int courseId, byte[] examFile);
    }
}
