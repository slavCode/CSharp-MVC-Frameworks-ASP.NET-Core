namespace CameraBazaar.Services
{
    using Data.Models.Enums;
    using Models;
    using System.Collections.Generic;

    public interface ICameraService
    {
        IEnumerable<CameraListingModel> All();

        CameraDetailsModel ById(int id);

        void Create(CameraAddModel addCameraModel, string userId);

        //void Edit(
        //    int id,
        //    CameraMake make,
        //    string model,
        //    decimal price,
        //    int quantity,
        //    int minShutterSpeed,
        //    int maxShutterSpeed,
        //    MinISO minIso,
        //    int maxIso,
        //    bool isFullFrame,
        //    string videoResolution,
        //    IEnumerable<LightMetering?> lightMeterings,
        //    string description,
        //    string imagUrl);

        void Edit(CameraEditModel cameraModel);

        void Delete(int id);
    }
}
