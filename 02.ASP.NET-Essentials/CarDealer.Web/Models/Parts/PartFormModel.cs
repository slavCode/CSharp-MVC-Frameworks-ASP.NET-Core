namespace CarDealer.Web.Models.Parts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PartFormModel
    {
        [Required] 
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Range(1, 200)]
        public int Quantity { get; set; }

        public IEnumerable<SelectListItem> AllSuppliers { get; set; }
    }
}
