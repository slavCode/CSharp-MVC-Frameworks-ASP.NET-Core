namespace LearningSystem.Web.Models.Courses
{
    using Service.Models.Courses;

    public class CourseDetailsViewModel
    {
        public CourseDetailsServiceModel Course { get; set; }

        public bool IsStudentInCourse { get; set; }
    }
}
