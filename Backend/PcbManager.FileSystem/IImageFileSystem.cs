using Microsoft.AspNetCore.Http;
using PcbManager.App;

namespace PcbManager.FileSystem
{
    public class ImageFileSystem : IImageFileSystem
    {
        public async Task SaveAsync(IFormFile file)
        {
            var path = Path.GetTempFileName();
            using var stream = File.Create(path);

            await file.CopyToAsync(stream);
        }
    }
}