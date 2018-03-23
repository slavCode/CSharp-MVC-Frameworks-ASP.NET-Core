namespace CarDealer.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    using Services;
    using Services.Models.Enums;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route(nameof(All) + "/{order}")]
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

        [Route("{id}")]
        public IActionResult ById(int id)
        {
            return this.ViewOrNotFound(this.customers.ById(id));
        }

        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            this.customers.Create(formModel.Name, formModel.BirthDate, formModel.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending });
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customers.ById(id);

            return View(new CustomerFormModel
            {
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            });
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(CustomerFormModel model, int id)
        {
            this.customers.Edit(id, model.Name, model.BirthDate, model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending });

        }
    }
}
