namespace CarDealer.Services.Models.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    public class SupplierModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Importer")]
        public bool IsImporter { get; set; }
    }
}
