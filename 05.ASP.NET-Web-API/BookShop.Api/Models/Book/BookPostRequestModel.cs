﻿namespace BookShop.Api.Models.Book
{
    using System.ComponentModel.DataAnnotations;

    public class BookPostRequestModel : BookPutRequestModel
    {
        [Required]
        public string Categories { get; set; }
    }
}
