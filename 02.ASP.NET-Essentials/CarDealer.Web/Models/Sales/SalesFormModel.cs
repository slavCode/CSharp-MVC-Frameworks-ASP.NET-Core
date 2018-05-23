namespace CarDealer.Web.Models.Sales
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SalesFormModel
    {
        [Display(Name = "Customer: ")]
        public IEnumerable<SelectListItem> AllCustomers { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Car: ")]
        public IEnumerable<SelectListItem> AllCars { get; set; }

        public int CarId { get; set; }

        [Display(Name = "Discount: ")]
        public double Discount { get; set; }   
    }
}
