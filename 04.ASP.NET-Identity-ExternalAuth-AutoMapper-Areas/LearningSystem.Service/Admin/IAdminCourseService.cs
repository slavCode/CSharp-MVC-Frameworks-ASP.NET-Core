namespace LearningSystem.Service.Admin
{
    using System;
    using System.Threading.Tasks;

    public interface IAdminCourseService
    {
        Task CreateAsync(string name,
            string description,
            DateTime endDate,
            DateTime startDate,
            string trainerId);
    }
}
