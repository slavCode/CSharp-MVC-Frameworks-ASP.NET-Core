namespace BookShop.Service.Models.Book
{
    using Core.Mapping;
    using Data.Models;

    public class BookWithTitleOnlyServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
