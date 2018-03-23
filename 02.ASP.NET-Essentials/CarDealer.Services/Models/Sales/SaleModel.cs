namespace CarDealer.Services.Models.Sales
{
    public class SaleModel
    {
        public int Id { get; set; }

        public string CarMake { get; set; }

        public string CarModel { get; set; }

        public long CarTravelledDistance { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public double DiscountedPrice { get; set; }

        public double Discount { get; set; }
    }
}
