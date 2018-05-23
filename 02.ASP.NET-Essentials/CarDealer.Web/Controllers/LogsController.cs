namespace CarDealer.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Implementaions;

    public class LogsController : Controller
    {
        private readonly ILogService logs;

        public LogsController(CarDealerDbContext db)
        {
            this.logs = new LogService(db);
        }

        public IActionResult All(string search)
        {
            if (search == null)
            {
                return View(this.logs.All());

            }

            return View(this.logs.ByUsername(search));

        }

        public IActionResult Delete()
        {
            this.logs.DeleteAll();

            return RedirectToAction(nameof(All));
        }
    }
}
