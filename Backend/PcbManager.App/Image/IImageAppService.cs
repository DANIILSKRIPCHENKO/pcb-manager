using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.App.Image
{
    public interface IImageAppService
    {
        public Task<Result<Domain.ImageNS.Image, BaseError>> UploadAsync(UploadImageRequest uploadImageRequest);

        public Task<Result<Domain.ImageNS.Image, BaseError>> GetByIdAsync(ImageId imageId);

        public Task<Result<List<Domain.ImageNS.Image>, BaseError>> GetAllAsync();

        public Task<Result<Domain.ImageNS.Image, BaseError>> DeleteAsync(ImageId imageId);
    }
}
