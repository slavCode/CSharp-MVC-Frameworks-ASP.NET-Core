namespace News.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound(this Controller controller, 
            object model, string message = "")
        {
            if (model == null) return controller.NotFound(message);

            return controller.Ok(model);
        }

        public static IActionResult OkOrNotFound(this Controller controller, bool result, string message = "")
        {
            if (!result) return controller.NotFound(message);

            return controller.Ok();
        }
    }
}
