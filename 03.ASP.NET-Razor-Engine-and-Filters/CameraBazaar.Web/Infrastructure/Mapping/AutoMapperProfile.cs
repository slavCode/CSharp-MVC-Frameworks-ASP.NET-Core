namespace CameraBazaar.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Services.Models;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CameraAddModel, Camera>()
                .ForMember(c => c.LightMetering, cam => cam.MapFrom(c => (LightMetering)c.LightMeterings.Cast<int>().Sum()));

            this.CreateMap<Camera, CameraDetailsModel>();

            this.CreateMap<CameraDetailsModel, CameraEditModel>();

        }
    }
}
