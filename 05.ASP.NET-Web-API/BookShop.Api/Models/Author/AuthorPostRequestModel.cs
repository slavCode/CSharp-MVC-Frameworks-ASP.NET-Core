namespace BookShop.Api.Models.Author
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.DataModelValidationConstants;

    public class AuthorPostRequestModel 
    {
        public int Id { get; set; }

        [Required]
        [MinLength(AuthorNameMinLength)]
        [MaxLength(AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(AuthorNameMinLength)]
        [MaxLength(AuthorNameMaxLength)]
        public string LastName { get; set; }
    }

}
