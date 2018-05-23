namespace BookShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataModelValidationConstants;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CategoryNameMinLength)]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public List<CategoryBooks> Books { get; set; } = new List<CategoryBooks>();

    }
}
