using Microsoft.AspNetCore.Http;

namespace PcbManager.App.Image
{
    public interface IImageFileSystem
    {
        public Task SaveAsync(IFormFile file);
    }
}
