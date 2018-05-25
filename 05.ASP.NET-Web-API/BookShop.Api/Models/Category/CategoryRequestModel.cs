namespace BookShop.Api.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.DataModelValidationConstants;

    public class CategoryRequestModel
    {
        [Required]
        [MinLength(CategoryNameMinLength)]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
