namespace LearningSystem.Service.Implementaions
{
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Users;
    using OpenHtmlToPdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.ServiceGlobulConstants;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;

        public UserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<UserProfileServiceModel> ByIdAsync(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>(new { studentId = id })
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserListingServiceModel>> FindByNameAsync(string term, int page)
            => await this.db
                .Users
                .Where(u => u.Name.ToLower().Contains(term.ToLower()))
                .Skip((page - 1) * UsersPageSize)
                .Take(UsersPageSize)
                .ProjectTo<UserListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalByNameAsync(string term)
            => await this.db
                .Users
                .Where(u => u.Name.ToLower().Contains(term.ToLower()))
                .CountAsync();

        public async Task<byte[]> GetCertificate(int courseId, string studentId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);

            if (studentInCourse == null) return null;

            var studentName = await this.GetStudentName(studentId);

            var certificateInfo = await this.db
                .Courses
                .Select(c => new
                {
                    CourseName = c.Name,
                    StartDate = c.StartDate.ToShortDateString(),
                    EndDate = c.EndDate.ToShortDateString(),
                    StudentName = studentName,
                    StudentGrade = studentInCourse.Grade.ToString(),
                    Trainer = c.Trainer.Name,
                    DateOfIssue = DateTime.UtcNow.ToShortDateString()
                })
                .FirstOrDefaultAsync();

            var certificateHtml = String.Format(ServiceGlobulConstants.Html,
                                        certificateInfo.CourseName,
                                        certificateInfo.StartDate,
                                        certificateInfo.EndDate,
                                        certificateInfo.StudentName,
                                        certificateInfo.StudentGrade,
                                        certificateInfo.Trainer,
                                        certificateInfo.DateOfIssue
            );

            return Pdf.From(certificateHtml).Content();
        }

        private async Task<string> GetStudentName(string studentId)
          => await this.db
                       .Users
                       .Where(u => u.Id == studentId)
                       .Select(u => u.Name)
                       .FirstOrDefaultAsync();
    }
}
