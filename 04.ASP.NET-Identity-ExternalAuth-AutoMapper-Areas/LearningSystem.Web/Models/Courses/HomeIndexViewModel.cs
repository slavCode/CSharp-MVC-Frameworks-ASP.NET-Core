namespace LearningSystem.Web.Models.Courses
{
    using System.ComponentModel.DataAnnotations;

    public class HomeIndexViewModel : CoursesWithPagingModel
    {
        public string Term { get; set; }

        [Display(Name = "Courses")]
        public bool InCourses { get; set; }

        [Display(Name = "Users")]
        public bool InUsers { get; set; }
    }
}
