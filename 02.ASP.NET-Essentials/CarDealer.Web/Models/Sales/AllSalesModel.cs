namespace CarDealer.Web.Models.Sales
{
    using Services.Models.Sales;
    using System.Collections.Generic;

    public class AllSalesModel
    {
        public IEnumerable<SaleModel> Sales { get; set; }
    }
}
