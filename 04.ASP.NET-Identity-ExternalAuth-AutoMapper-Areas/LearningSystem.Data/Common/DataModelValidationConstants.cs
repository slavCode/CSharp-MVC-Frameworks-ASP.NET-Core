namespace LearningSystem.Data.Common
{
    public static class DataModelValidationConstants
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 80;
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 20;

        public const int CourseNameMinLength = 2;
        public const int CourseNameMaxLength = 50;
        public const int CourseDescriptionMinLength = 10;
        public const int CourseDescriptionMaxLength = 100;

        public const int ArticleTitleMinLength = 5;
        public const int ArticleTitleMaxLength = 50;
        public const int ArticleContentMinLength = 10;

        public const int ExamSubmissionLength = 2 * 1024 * 1024;
    }
}
