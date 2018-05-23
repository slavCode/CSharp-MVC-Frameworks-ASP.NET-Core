namespace LearningSystem.Service
{
    using Models.Courses;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Models;
    using Models.Users;

    public interface ITrainerService
    {
        Task<bool> IsTrainerInCourse(string trainerId, int courseId);

        Task<int> TotalStudentsAsync(int courseId);

        Task<IEnumerable<TrainerCourseListingServiceModel>> CoursesAsync(string trainerId);

        Task<IEnumerable<StudentInCourseServiceModel>> StudentsAsync(int courseId, int page);

        Task<bool> GradeStudentAsync(string studentId, Grade grade, int courseId);

        Task<byte[]> DownloadExam(string studentId, int courseId);

        Task<StudentAndCourseNamesServiceModel> GetNamesAsync(string studentId, int courseId);
    }
}
