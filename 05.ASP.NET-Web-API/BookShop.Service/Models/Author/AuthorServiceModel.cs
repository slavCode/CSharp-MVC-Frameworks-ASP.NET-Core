namespace BookShop.Service.Models.Author
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorServiceModel : IMapFrom<Author>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<Author, AuthorServiceModel>()
                .ForMember(a => a.Books, cfg => cfg
                    .MapFrom(a => a.Books.Select(b => b.Title)));
    }
}
