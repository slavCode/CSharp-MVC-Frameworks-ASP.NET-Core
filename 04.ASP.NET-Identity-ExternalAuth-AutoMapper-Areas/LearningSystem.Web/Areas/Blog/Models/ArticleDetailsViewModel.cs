namespace LearningSystem.Web.Areas.Blog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ArticleDetailsViewModel : PublishArticleFormModel
    {
        [Required]
        public string Author { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }
    }
}
