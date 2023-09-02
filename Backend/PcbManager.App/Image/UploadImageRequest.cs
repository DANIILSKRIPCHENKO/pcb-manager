using Microsoft.AspNetCore.Http;

namespace PcbManager.App.Image
{
    public class UploadImageRequest
    {
        public IFormFile ImageFile { get; set; }

        public Guid UserId { get; set; }
    }
}
