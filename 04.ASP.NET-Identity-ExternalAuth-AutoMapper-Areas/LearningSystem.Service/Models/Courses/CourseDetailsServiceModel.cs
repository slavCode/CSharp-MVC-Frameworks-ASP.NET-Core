namespace LearningSystem.Service.Models.Courses
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System;

    public class CourseDetailsServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Students { get; set; }

        public bool IsActive => this.StartDate >= DateTime.UtcNow;

        public void ConfigureMapping(Profile mapper)
           => mapper.CreateMap<Course, CourseDetailsServiceModel>()
                        .ForMember(u => u.Trainer, cfg => cfg.MapFrom(u => u.Trainer.Name))
                        .ForMember(c => c.Students, cfg => cfg.MapFrom(s => s.Students.Count))
                        .ForMember(c => c.IsActive, 
                                            cfg => cfg.MapFrom(c => c.StartDate <= DateTime.UtcNow));
    }
}
