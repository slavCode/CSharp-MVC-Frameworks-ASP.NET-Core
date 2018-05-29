namespace News.Test
{
    using Api.Infrastructure.Mapper;
    using AutoMapper;

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
