namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Sales;
    using Services;

    public class SalesController: Controller
    {
        private readonly ISalesService sales;

        public SalesController(ISalesService sales)
        {
            this.sales = sales;
        }

        [Route("sales")]
        public IActionResult All()
        {
            var allSales = this.sales.All();

            return this.View(new AllSalesModel
            {
                Sales = allSales
            });
        }

        [Route("sales/{id}")]
        public IActionResult ById(string id)
        {
            var saleId = int.Parse(id);

            var saleById = this.sales.ById(saleId);

            return this.View(new ByIdSalesModel
            {
                ById = saleById,
                SaleId = saleId
            });
        }

        [Route("sales/discounted")]
        public IActionResult Discounted()
        {
            var discountedSales = this.sales.Discounted();

            return this.View(new AllSalesModel
            {
                Sales = discountedSales
            });
        }

        [Route("sales/discounted/{percent}")]
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
    }
}
