namespace CarDealer.Services.Models.Sales
{
    public class SaleReviewModel
    {
        public string Customer { get; set; }

        public int CustomerId { get; set; }

        public string Car { get; set; }

        public int CarId { get; set; }

        public string DiscountAsString { get; set; }

        public double Discount { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }
    }
}
