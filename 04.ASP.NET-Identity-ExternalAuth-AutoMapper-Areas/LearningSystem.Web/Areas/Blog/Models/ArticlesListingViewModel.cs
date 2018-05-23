namespace LearningSystem.Web.Areas.Blog.Models
{
    using Service.Common;
    using Service.Models.Articles;
    using System;
    using System.Collections.Generic;

    public class ArticlesListingViewModel
    {
        public int TotalArticles { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalArticles / ServiceGlobulConstants.ArticlesPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages
                             ? this.TotalPages
                             : this.CurrentPage + 1;

        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

        public string SearchTerm { get; set; }
    }
}
