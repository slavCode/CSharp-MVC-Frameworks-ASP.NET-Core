namespace LearningSystem.Service.Models.Courses
{
    using System;
    using Core.Mapping;
    using Data.Models;

    public class TrainerCourseListingServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
