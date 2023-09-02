using Microsoft.AspNetCore.Http;
using PcbManager.Main.App.Image;

namespace PcbManager.Main.FileSystem
{
    public class ImageFileSystem : IImageFileSystem
    {
        public async Task SaveAsync(IFormFile file)
        {
            const string fileSystemPath = "C:/Users/Danil/Desktop/Images";
            await using var stream = File.Create($"{fileSystemPath}/{file.FileName}");
            await file.CopyToAsync(stream);
        }
    }
}