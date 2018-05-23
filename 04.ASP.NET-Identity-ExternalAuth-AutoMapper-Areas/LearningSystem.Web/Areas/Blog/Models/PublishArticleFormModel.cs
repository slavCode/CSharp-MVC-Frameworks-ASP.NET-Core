namespace LearningSystem.Web.Areas.Blog.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.DataModelValidationConstants;

    public class PublishArticleFormModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        public string Content { get; set; }
    }
}
