namespace LearningSystem.Service.Models.Courses
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System;

    public class CourseListingServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void ConfigureMapping(Profile mapper)
          => mapper.CreateMap<Course, CourseListingServiceModel>()
                .ForMember(u => u.Trainer, cfg => cfg.MapFrom(u => u.Trainer.Name));
    }
}
