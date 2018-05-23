namespace BookShop.Service.Models.Book
{
    using AutoMapper;
    using Data.Models;
    using System.Linq;

    public class BookWithAuthorServiceModel : BookServiceModel
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<Book, BookWithAuthorServiceModel>()
           .ForMember(b => b.Categories, cfg =>
               cfg.MapFrom(b => b.Categories.Select(c => c.Category.Name)))
           .ForMember(b => b.Author, cfg => 
               cfg.MapFrom(b => b.Author.FirstName + " " + b.Author.LastName));
    }
}
