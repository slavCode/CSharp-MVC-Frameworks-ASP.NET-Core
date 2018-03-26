namespace CarDealer.Services
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISalesService
    {
        SaleModel ById(int id);

        SaleReviewModel SaleReview(int carId, int customerId, double discount);

        IEnumerable<SaleModel> All();

        IEnumerable<SaleModel> Discounted();

        IEnumerable<SaleModel> DiscountedByPercent(double percent);

        void Create(int carId, int customerId, double discount);
    }
}
