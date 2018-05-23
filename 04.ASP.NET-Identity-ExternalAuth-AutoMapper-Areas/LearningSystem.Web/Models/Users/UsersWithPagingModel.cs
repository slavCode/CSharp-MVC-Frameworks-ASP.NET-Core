namespace LearningSystem.Web.Models.Users
{
    using Service.Common;
    using Service.Models.Users;
    using System;
    using System.Collections.Generic;

    public class UsersWithPagingModel
    {
        public int TotalUsers { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling(this.TotalUsers / (double)ServiceGlobulConstants.UsersPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1
            ? 1
            : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages
            ? this.TotalPages
            : this.CurrentPage + 1;

        public IEnumerable<UserListingServiceModel> Users { get; set; }
            = new List<UserListingServiceModel>();
    }
}
