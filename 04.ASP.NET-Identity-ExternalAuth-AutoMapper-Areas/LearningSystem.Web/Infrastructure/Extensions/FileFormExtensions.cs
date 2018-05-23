namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public static class FileFormExtensions
    {
        public static byte[] Read(this IFormFile file)
        {
            using (var memory = new MemoryStream())
            {
                file.CopyToAsync(memory);
                
                return memory.ToArray();
            }
        }
    }
}
