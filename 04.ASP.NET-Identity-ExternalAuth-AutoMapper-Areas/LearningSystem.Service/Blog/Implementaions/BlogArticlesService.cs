namespace LearningSystem.Service.Blog.Implementaions
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Articles;
    using static Common.ServiceGlobulConstants;

    public class 
        BlogArticlesService : IBlogArticlesService
    {
        private readonly LearningSystemDbContext db;

        public BlogArticlesService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();

        public async Task<int> TotalFromSearchAsync(string searchTerm)
            => await this.db
                .Articles
                .Where(a => a.Title.ToLower().Contains(searchTerm.ToLower())
                            || a.Content.ToLower().Contains(searchTerm.ToLower()))
                .CountAsync();

        public Task<ArticleDetailsServiceModel> ByIdAsync(int id)
            => this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page)
           => await this.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * ArticlesPageSize)
                .Take(ArticlesPageSize)
                .ProjectTo<ArticleListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<ArticleListingServiceModel>> SearchAsync(int page, string searchTerm)
            => await this.db
                .Articles
                .Where(a => a.Title.ToLower().Contains(searchTerm.ToLower()) 
                             || a.Content.ToLower().Contains(searchTerm.ToLower()))
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * ArticlesPageSize)
                .Take(ArticlesPageSize)
                .ProjectTo<ArticleListingServiceModel>()
                .ToListAsync();

        public async Task Create(string title, string content, string authorId)
        {
            var article = new Article
            {
                AuthorId = authorId,
                Content = content,
                Title = title,
                PublishDate = DateTime.UtcNow
            };

            this.db.Add(article);
            await this.db.SaveChangesAsync();
        }

    }
}
