namespace CameraBazaar.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserDetailsModel : UserModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\+[0-9]{10,12}")]
        public string Phone { get; set; }

        public int InStockCameras { get; set; }

        public int OutOfStockCameras { get; set; }

        public IEnumerable<CameraListingModel> Cameras { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
