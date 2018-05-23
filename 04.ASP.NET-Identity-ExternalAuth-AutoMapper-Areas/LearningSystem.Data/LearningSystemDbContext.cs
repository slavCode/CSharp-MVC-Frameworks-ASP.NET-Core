namespace LearningSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<User>()
                .HasMany(s => s.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(s => s.StudentId);

            builder
                .Entity<User>()
                .HasMany(t => t.Trainings)
                .WithOne(tr => tr.Trainer)
                .HasForeignKey(t => t.TrainerId);

            builder
                .Entity<User>()
                .HasMany(s => s.Articles)
                .WithOne(a => a.Author)
                .HasForeignKey(s => s.AuthorId);

            builder
                .Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(sc => sc.Course)
                .HasForeignKey(c => c.CourseId);

            base.OnModelCreating(builder);
        }
    }
}
