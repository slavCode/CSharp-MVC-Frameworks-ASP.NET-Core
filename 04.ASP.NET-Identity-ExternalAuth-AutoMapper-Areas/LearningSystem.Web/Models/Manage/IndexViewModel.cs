namespace LearningSystem.Web.Models.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.DataModelValidationConstants;

    public class IndexViewModel
    {
        [MinLength(UsernameMinLength)]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public DateTime Birthday { get; set; }  

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
