using Microsoft.AspNetCore.Http;

namespace PcbManager.Main.App.Image
{
    public class UploadImageRequest
    {
        public IFormFile ImageFile { get; set; }

        public Guid UserId { get; set; }
    }
}
