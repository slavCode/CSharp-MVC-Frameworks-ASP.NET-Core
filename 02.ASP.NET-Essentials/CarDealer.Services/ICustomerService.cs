namespace CarDealer.Services
{
    using Models.Customers;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        void Create(string name, DateTime birthday, bool isYoungDriver);

        void Edit(int id, string name, DateTime birthday, bool isYoungDriver);

        CustomerModel ById(int id);
    }
}
