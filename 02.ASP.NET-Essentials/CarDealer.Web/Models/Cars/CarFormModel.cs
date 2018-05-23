namespace CarDealer.Web.Models.Cars
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Models.Cars;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel : CarModel
    {
        [Display(Name = "All Parts")]
        public IEnumerable<SelectListItem> AllParts { get; set; }

        public IList<int> PartIds { get; set; }
    }
}
