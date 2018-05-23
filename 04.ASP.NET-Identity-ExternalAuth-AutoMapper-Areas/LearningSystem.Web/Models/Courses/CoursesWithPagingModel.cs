namespace LearningSystem.Web.Models.Courses
{
    using Service.Common;
    using Service.Models.Courses;
    using System;
    using System.Collections.Generic;

    public class CoursesWithPagingModel
    {
        public int TotalCourses { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling(this.TotalCourses / (double)ServiceGlobulConstants.CoursesPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1
            ? 1
            : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages
            ? this.TotalPages
            : this.CurrentPage + 1;

        public IEnumerable<CourseListingServiceModel> Courses { get; set; } = new List<CourseListingServiceModel>();
    }
}
