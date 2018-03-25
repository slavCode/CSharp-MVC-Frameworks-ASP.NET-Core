namespace CarDealer.Services
{
    using Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartListingModel> All(int pageSize = 10, int page = 1);

        int Total();

        void Create(string name, decimal price, int supplierId, int quantity);

        void Delete(int id);

        void Edit(int id, decimal price, int quntity);

        PartListingModel ById(int id);
    }
}
