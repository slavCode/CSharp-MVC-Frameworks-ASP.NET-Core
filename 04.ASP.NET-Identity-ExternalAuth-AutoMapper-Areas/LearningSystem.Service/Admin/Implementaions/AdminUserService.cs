namespace LearningSystem.Service.Admin.Implementaions
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Users;

    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserListingServiceModel>> AllAsync()
            =>  await this.db
                .Users
                .ProjectTo<UserListingServiceModel>()
                .ToListAsync();
    }
}
