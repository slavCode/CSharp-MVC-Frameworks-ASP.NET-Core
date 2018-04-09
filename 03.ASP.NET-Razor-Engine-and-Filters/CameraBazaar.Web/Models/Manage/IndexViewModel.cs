namespace CameraBazaar.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string NewPassword { get; set; }

        [Phone]
        [Display(Name = "Phone: ")]
        [RegularExpression(@"\+[0-9]{10,12}")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password: ")]
        public string OldPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
