namespace LearningSystem.Service.Models.Users
{
    using AutoMapper;
    using Core.Mapping;
    using Courses;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserProfileCourseServiceModel> Courses { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<User, UserProfileServiceModel>()
                .ForMember(s => s.Courses, cfg => cfg
                    .MapFrom(s => s.Courses.Select(c => c.Course)));
        }
    }
}
