namespace LearningSystem.Service.Html.Implementaions
{
    using Ganss.XSS;
    using Html;

    public class HtmlService : IHtmlService
    {
        private readonly IHtmlSanitizer sanitizer;
        public HtmlService()
        {
            this.sanitizer = new HtmlSanitizer();
            this.sanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string html)
            => this.sanitizer.Sanitize(html);
    }
}
