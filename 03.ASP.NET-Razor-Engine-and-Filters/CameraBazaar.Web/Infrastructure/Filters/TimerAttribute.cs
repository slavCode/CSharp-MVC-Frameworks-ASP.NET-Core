namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class TimerAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch stopwatch;
        private DateTime startTime;

        public TimerAttribute()
        {
            this.stopwatch = new Stopwatch();
            this.startTime = new DateTime();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopwatch.Start();
            this.startTime = DateTime.UtcNow.ToLocalTime();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopwatch.Stop();

            using (var writer = new StreamWriter("action-times.txt", true))
            {
                var controller = context.RouteData.Values["controller"];
                var action = context.RouteData.Values["action"];

                var line = $"{startTime} – {controller}Controller.{action} – {this.stopwatch.Elapsed}";

                writer.WriteLine(line);
            }
        }
    }
}
