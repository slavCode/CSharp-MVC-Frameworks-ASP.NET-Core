namespace CarDealer.Services
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISalesService
    {
        SaleModel ById(int id);

        IEnumerable<SaleModel> All();

        IEnumerable<SaleModel> Discounted();

        IEnumerable<SaleModel> DiscountedByPercent(double percent);
    }
}
