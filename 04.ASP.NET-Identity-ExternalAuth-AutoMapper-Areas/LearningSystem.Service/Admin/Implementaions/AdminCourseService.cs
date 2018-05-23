namespace LearningSystem.Service.Admin.Implementaions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly LearningSystemDbContext db;

        public AdminCourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name,
            string description,
            DateTime endDate,
            DateTime startDate,
            string trainerId)
        {
            var trainerExists = await this.db.Users.AnyAsync(t => t.Id == trainerId);

            if (!trainerExists)
            {
                return;
            }

            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            await this.db.AddAsync(course);
            await this.db.SaveChangesAsync();
        }
    }
}
