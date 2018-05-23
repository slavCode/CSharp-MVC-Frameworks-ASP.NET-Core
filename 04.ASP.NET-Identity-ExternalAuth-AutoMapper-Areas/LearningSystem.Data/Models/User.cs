namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataModelValidationConstants;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(NameMinLength)]
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public List<Course> Trainings { get; set; } = new List<Course>();

        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
