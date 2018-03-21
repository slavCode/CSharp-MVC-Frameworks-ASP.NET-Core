namespace CarDealer.Services
{
    using Models.Customers;
    using Models.Enums;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerByIdModel CustomerById(int id);
    }
}
