namespace CarDealer.Web.Models.Sales
{
    using Services.Models.Sales;

    public class ByIdSalesModel
    {
        public int SaleId { get; set; }

        public SaleModel ById { get; set; }
    }
}
