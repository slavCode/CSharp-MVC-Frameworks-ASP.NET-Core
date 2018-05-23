namespace CameraBazaar.Web.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditUserFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{10,12}")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
