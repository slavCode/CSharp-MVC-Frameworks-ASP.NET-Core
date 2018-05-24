namespace BookShop.Service.Models.Category
{
    using Core.Mapping;
    using Data.Models;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
