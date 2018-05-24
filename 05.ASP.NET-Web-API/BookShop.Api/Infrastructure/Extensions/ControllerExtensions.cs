namespace BookShop.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound(this Controller controller, object model)
        {
            if (model == null) return controller.NotFound();

            return controller.Ok(model);
        }

        public static IActionResult OkOrBadRequest(this Controller controller, bool success)
        {
            if (!success) return controller.BadRequest();

            return controller.Ok();
        }
    }
}
