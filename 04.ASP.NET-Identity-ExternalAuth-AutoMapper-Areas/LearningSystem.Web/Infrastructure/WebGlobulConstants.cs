namespace LearningSystem.Web.Infrastructure
{
    public class WebGlobulConstants
    {
        public const string AdminArea = "Admin";
        public const string BlogArea = "Blog";
        public const string TrainerArea = "Trainer";


        public const string AdministratorRole = "Administrator";
        public const string TrainerRole = "Trainer";
        public const string AuthorRole = "Author";

        public const string AdminEmail = "admin@admin.bg";
        public const string AdminUsername = "admin@admin.bg";
        public const string AdminName = "admin";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const string AuthenicatorUriFormat =
            "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
    }
}
