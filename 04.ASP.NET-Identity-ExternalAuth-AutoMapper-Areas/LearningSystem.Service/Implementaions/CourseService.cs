namespace LearningSystem.Service.Implementaions
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Courses;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.ServiceGlobulConstants;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<int> TotalAsync()
            => await this.db.Courses.CountAsync();

        public async Task<TModel> ByIdAsync<TModel>(int id)
            => await this.db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> IsStudentInCourseAsync(string studentId, int courseId)
            => await this.db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.Students.Any(s => s.StudentId == studentId));

        public async Task<IEnumerable<CourseListingServiceModel>> AllAsync(int page)
            => await this.db
                .Courses
                .OrderByDescending(c => c.StartDate)
                .Skip((page - 1) * CoursesPageSize)
                .Take(CoursesPageSize)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<bool> SignInStudentAsync(string studentId, int courseId)
        {
            var courseInfo = await this.GetCourseInfo(studentId, courseId);

            if (courseInfo == null
                || courseInfo.IsStudentEnrolled
                || courseInfo.StartDate < DateTime.UtcNow)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await this.db.AddAsync(studentCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutStudentAsync(string studentId, int courseId)
        {
            var courseInfo = await this.GetCourseInfo(studentId, courseId);

            if (courseInfo == null || !courseInfo.IsStudentEnrolled)
            {
                return false;
            }

            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);
            this.db.Remove(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> FindByNameAsync(string term, int page)
            => await this.db
                .Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.Name.ToLower()
                .Contains(term.ToLower()))
                .Skip((page - 1) * CoursesPageSize)
                .Take(CoursesPageSize)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalByNameAsync(string term)
            => await this.db
                .Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.Name.ToLower()
                .Contains(term.ToLower()))
                .CountAsync();

        public async Task<bool> AddSubmissionAsync(string studentId, int courseId, byte[] examFile)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);

            if (studentInCourse == null || examFile == null)
            {
                return false;
            }

            studentInCourse.ExamSubmission = examFile;

            await this.db.SaveChangesAsync();

            return true;
        }

        private Task<CourseInfoServiceModel> GetCourseInfo(string studentId, int courseId)
            => this.db
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new CourseInfoServiceModel
                {
                    StartDate = c.StartDate,
                    IsStudentEnrolled = c.Students.Any(s => s.StudentId == studentId)
                })
                .FirstOrDefaultAsync();
    }
}
