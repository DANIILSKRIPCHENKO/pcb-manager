using Microsoft.AspNetCore.Http;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.App.Image
{
    public interface IImageFileSystem
    {
        public void CreateFolder(UserId userId);

        public void DeleteFolder(UserId userId);

        public Task SaveAsync(UserId userId, IFormFile file);
    }
}
