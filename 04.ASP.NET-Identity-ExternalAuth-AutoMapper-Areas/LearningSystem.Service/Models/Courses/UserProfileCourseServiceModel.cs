namespace LearningSystem.Service.Models.Courses
{
    using System;
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Linq;

    public class UserProfileCourseServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }

        public DateTime EndDate { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            string studentId = null;
            mapper.CreateMap<Course, UserProfileCourseServiceModel>()
                .ForMember(c => c.Grade, cfg => cfg
                    .MapFrom(c => c.Students
                        .Where(s => s.StudentId == studentId)
                        .Select(s => s.Grade)
                        .FirstOrDefault()));
        }
    }
}
