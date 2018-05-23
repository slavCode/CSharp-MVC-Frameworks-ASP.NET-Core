namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (var writer = new StreamWriter("logs.txt", true))
            {
                var timestamp = DateTime.Now;
                var ip = context.HttpContext.Connection.RemoteIpAddress;
                var user = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                var controller = context.RouteData.Values["controller"];
                var action = context.RouteData.Values["action"];

                var result = $"{timestamp} – {ip} – {user} – {controller}Controller.{action}";

                if (context.Exception != null)
                {
                    result = $"[!] {result} - {context.Exception.GetType()} – {context.Exception.Message}";
                }

                writer.WriteLine(result);
            }
        }
    }
}
