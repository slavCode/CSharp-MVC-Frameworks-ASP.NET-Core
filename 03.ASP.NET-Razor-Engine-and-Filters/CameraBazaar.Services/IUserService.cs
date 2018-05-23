namespace CameraBazaar.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        UserDetailsModel ById(string id);

        IEnumerable<UserModel> All();

        void AddLoginTime(string username);
    }
}
