using Microsoft.AspNetCore.Http;
using PcbManager.Main.App.Image;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.FileSystem
{
    public class ImageFileSystem : IImageFileSystem
    {
        private readonly string _fileSystemPath = "C:/Users/Danil/Desktop/Images";

        public void CreateFolder(UserId userId) =>
            Directory.CreateDirectory($"{_fileSystemPath}/{userId.Value}");

        public void DeleteFolder(UserId userId) =>
            Directory.Delete($"{_fileSystemPath}/{userId.Value}");

        public async Task SaveAsync(UserId userId, IFormFile file)
        {
            await using var stream = File.Create(
                $"{_fileSystemPath}/{userId.Value}/{file.FileName}"
            );
            await file.CopyToAsync(stream);
        }
    }
}
