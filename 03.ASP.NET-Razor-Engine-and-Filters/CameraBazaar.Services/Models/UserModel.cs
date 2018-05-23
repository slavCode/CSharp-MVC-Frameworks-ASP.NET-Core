namespace CameraBazaar.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Username { get; set; }
    }
}
