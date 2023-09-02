using Microsoft.AspNetCore.Http;

namespace PcbManager.Main.App.Image
{
    public interface IImageFileSystem
    {
        public Task SaveAsync(IFormFile file);
    }
}
