namespace BookShop.Service.Models.Book
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BookServiceModel : IMapFrom<Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public int Edition { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public virtual void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<Book, BookServiceModel>()
                .ForMember(b => b.Categories, cfg =>
                    cfg.MapFrom(b => b.Categories.Select(c => c.Category.Name)));
    }
}
