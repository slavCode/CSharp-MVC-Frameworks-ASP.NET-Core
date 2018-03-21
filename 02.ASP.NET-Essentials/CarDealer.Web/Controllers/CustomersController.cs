namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    using Services;
    using Services.Models.Enums;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("customers/all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "descending"
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var orderedCutomers = this.customers.OrderedCustomers(orderDirection);

            return View(new AllCustomersModel
            {
                OrderDirection = orderDirection,
                Customers = orderedCutomers
            });
        }

        [Route("customers/{id}")]
        public IActionResult ById(string id)
        {
            var customerId = int.Parse(id);

            var customer = this.customers.CustomerById(customerId);

            return this.View(new CustomerByIdDetailsModel()
            {
                Customer = customer
            });
        }
    }
}
