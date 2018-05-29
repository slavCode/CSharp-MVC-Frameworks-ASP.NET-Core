namespace News.Service.Models
{
    using Core.Mapping;
    using Data.Models;
    using System;

    public class ItemServiceModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
