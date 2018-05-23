namespace CarDealer.Web.Models.Sales
{
    public class SaleReviewModel
    {
        public string Customer { get; set; }

        public string Car { get; set; }

        public double Discount { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }
    }
}
