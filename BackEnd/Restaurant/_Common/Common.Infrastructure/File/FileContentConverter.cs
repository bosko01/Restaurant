using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.File
{
    public static class FileContentConverter
    {
        public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
