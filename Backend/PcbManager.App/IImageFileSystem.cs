using Microsoft.AspNetCore.Http;

namespace PcbManager.App
{
    public interface IImageFileSystem
    {
        public Task SaveAsync(IFormFile file);
    }
}
