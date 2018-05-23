namespace LearningSystem.Service.Implementaions
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Courses;
    using Models.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using static Common.ServiceGlobulConstants;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> IsTrainerInCourse(string trainerId, int courseId)
            => await this.db
                .Courses
                .AnyAsync(c => c.TrainerId == trainerId && c.Id == courseId);

        public async Task<int> TotalStudentsAsync(int courseId)
            => await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(s => s.Student)).CountAsync();


        public async Task<IEnumerable<TrainerCourseListingServiceModel>> CoursesAsync(string trainerId)
            => await this.db
                .Courses
                .Where(c => c.TrainerId == trainerId)
                .ProjectTo<TrainerCourseListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<StudentInCourseServiceModel>> StudentsAsync(int courseId, int page)
            => await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(s => s.Student))
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * UsersPageSize)
                .Take(UsersPageSize)
                .ProjectTo<StudentInCourseServiceModel>(new { courseId })
                .ToListAsync();


        public async Task<bool> GradeStudentAsync(string studentId, Grade grade, int courseId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> DownloadExam(string studentId, int courseId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);

            return studentInCourse?.ExamSubmission;
        }

        public async Task<StudentAndCourseNamesServiceModel> GetNamesAsync(string studentId, int courseId)
            => new StudentAndCourseNamesServiceModel
            {
                Username = await this.db
                            .Users
                            .Where(u => u.Id == studentId)
                            .Select(u => u.UserName)
                            .FirstOrDefaultAsync(),
                CourseName = await this.db
                            .Courses.Where(c => c.Id == courseId)
                            .Select(c => c.Name)
                            .FirstOrDefaultAsync()
            };

    }
}
