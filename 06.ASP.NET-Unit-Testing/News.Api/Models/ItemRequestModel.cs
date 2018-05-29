namespace News.Api.Models
{
    using Core.Mapping;
    using Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ItemRequestModel : IMapFrom<Item>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
