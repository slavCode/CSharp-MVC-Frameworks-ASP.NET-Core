namespace CarDealer.Web.Models.Sales
{
    using Services.Models.Sales;
    using System.Collections.Generic;

    public class DiscountedByPercentSalesModel
    {
        public double Percent { get; set; }

        public IEnumerable<SaleModel> Sales { get; set; }
    }
}
