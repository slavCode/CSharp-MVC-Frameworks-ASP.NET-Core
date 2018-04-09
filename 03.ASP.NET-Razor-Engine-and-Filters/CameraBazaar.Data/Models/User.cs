namespace CameraBazaar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public IList<Camera> Cameras { get; set; } = new List<Camera>();
    }
}
