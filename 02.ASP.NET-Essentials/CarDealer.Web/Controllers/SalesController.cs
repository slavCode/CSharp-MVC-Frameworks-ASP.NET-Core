namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Sales;
    using Services;
    using Services.Models.Enums;
    using Services.Models.Sales;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISalesService sales;
        private readonly ICarService cars;
        private readonly ICustomerService customers;

        public SalesController(ISalesService sales, ICarService cars, ICustomerService customers)
        {
            this.sales = sales;
            this.cars = cars;
            this.customers = customers;
        }

        [Route("")]
        public IActionResult All()
        {
            var allSales = this.sales.All();

            return this.View(new AllSalesModel
            {
                Sales = allSales
            });
        }

        [Route("{id}")]
        public IActionResult Details(string id)
        {
            var saleId = int.Parse(id);

            var saleById = this.sales.ById(saleId);

            return this.View(new ByIdSalesModel
            {
                ById = saleById,
                SaleId = saleId
            });
        }

        [Route(nameof(Discounted))]
        public IActionResult Discounted()
        {
            var discountedSales = this.sales.Discounted();

            return this.View(new AllSalesModel
            {
                Sales = discountedSales
            });
        }

        [Route("discounted/{percent}")]
        public IActionResult DiscountedByPercent(string percent)
        {
            var percentAsDouble = double.Parse(percent);

            var discountedSales = this.sales.DiscountedByPercent(percentAsDouble);

            return this.View(new DiscountedByPercentSalesModel()
            {
                Percent = percentAsDouble,
                Sales = discountedSales
            });
        }

        [Authorize]
        [Route("create")]
        public IActionResult Create()
        {
            return View(new SalesFormModel
            {
                AllCars = this.cars
                    .All()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Make + " " + c.Model,
                        Value = c.Id.ToString()
                    }),
                AllCustomers = this.customers
                    .OrderedCustomers(OrderDirection.Ascending)
                    .Select(cu => new SelectListItem
                    {
                        Text = cu.Name,
                        Value = cu.Id.ToString()
                    })
            });
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public IActionResult Create(SalesFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            var sale = this.sales.SaleReview(formModel.CarId, formModel.CustomerId, formModel.Discount);

            return RedirectToAction(nameof(Review), formModel);
        }

        [Authorize]
        [Route("review")]
        public IActionResult Review(SalesFormModel formModel)
        {
            var sale = this.sales.SaleReview(formModel.CarId, formModel.CustomerId, formModel.Discount);

            return View(sale);

        }

        [Authorize]
        [HttpPost]
        [Route("review")]
        public IActionResult Review(SaleReviewModel saleReviewModel)
        {
            this.sales.Create(saleReviewModel.CarId, saleReviewModel.CustomerId, saleReviewModel.Discount);

            return RedirectToAction(nameof(All));
        }
    }
}
