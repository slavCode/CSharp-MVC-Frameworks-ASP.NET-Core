namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Service.Models.Users;
    using System.Collections.Generic;

    public class AdminUsersListingViewModel
    {
        public IEnumerable<UserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
