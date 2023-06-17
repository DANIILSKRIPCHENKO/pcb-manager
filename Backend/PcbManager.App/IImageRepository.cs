using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.App
{
    public interface IImageRepository
    {
        public Task<Result<Image>> CreateAsync(Image image);

        public Task<Maybe<Image>> GetByIdAsync(ImageId imageId);

        public Task<Maybe<List<Image>>> GetAllAsync();
    }
}
