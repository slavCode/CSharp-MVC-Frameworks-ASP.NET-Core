﻿namespace CarDealer.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}