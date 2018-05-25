namespace BookShop.Api.Models.Book
{
    using System.ComponentModel.DataAnnotations;

    public class BookCreateRequestModel : BookEditRequestModel
    {
        [Required]
        public string Categories { get; set; }
    }
}
