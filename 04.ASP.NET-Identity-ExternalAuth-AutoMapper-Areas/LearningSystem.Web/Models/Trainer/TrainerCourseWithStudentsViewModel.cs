namespace LearningSystem.Web.Models.Trainer
{
    using Service.Common;
    using Service.Models.Courses;
    using Service.Models.Users;
    using System;
    using System.Collections.Generic;

    public class TrainerCourseWithStudentsViewModel
    {
        public int TotalStudents { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalStudents / ServiceGlobulConstants.UsersPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages
            ? this.TotalPages
            : this.CurrentPage + 1;

        public TrainerCourseListingServiceModel Course { get; set; }

        public IEnumerable<StudentInCourseServiceModel> Students { get; set; } 
    }
}
