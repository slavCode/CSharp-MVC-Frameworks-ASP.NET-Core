namespace CarDealer.Services
{
    using Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartModel> ByCarId(int carId);
    }
}
