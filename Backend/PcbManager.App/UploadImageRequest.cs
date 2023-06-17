using Microsoft.AspNetCore.Http;

namespace PcbManager.App
{
    public class UploadImageRequest
    {
        public IFormFile ImageFile { get; set; }

        public Guid UserId { get; set; }
    }
}
