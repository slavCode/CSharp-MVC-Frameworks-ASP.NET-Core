namespace News.Test
{
    using AutoMapper;
    using News.Api.Infrastructure.Mapper;

    public class Tests
    {
        private static bool testsIntialized;

        public static void Initialize()
        {
            if (!testsIntialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsIntialized = true;
            }
        }
    }
}
