using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.App
{
    public interface IImageAppService
    {
        public Task<Result<Image>> UploadAsync(UploadImageRequest uploadImageRequest);

        public Task<Maybe<Image>> GetByIdAsync(ImageId imageId);

        public Task<Maybe<List<Image>>> GetAllAsync();
    }
}
