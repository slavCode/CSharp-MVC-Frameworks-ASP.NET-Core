namespace BookShop.Service.Infrastructure
{
    public static class StringExtensions
    {
        public static string Capitalize(this string word)
            => word[0].ToString().ToUpper() + word.Substring(1);
    }
}
