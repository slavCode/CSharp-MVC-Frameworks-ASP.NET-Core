namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.DataModelValidationConstants;

    public class CreateCourseFormModel : IValidatableObject
    {
        [Required]
        [MinLength(CourseNameMinLength)]
        [MaxLength(CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(CourseDescriptionMinLength)]
        [MaxLength(CourseDescriptionMaxLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = WebGlobulConstants.TrainerRole)]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult("The Start date must be in the future.");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("End date should be after start date.");
            }
        }
    }
}
