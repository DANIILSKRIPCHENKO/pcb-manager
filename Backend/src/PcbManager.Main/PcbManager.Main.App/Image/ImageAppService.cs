using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.App.Image
{
    public class ImageAppService : IImageAppService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageFileSystem _imageFileSystem;

        public ImageAppService(IImageRepository imageRepository, IImageFileSystem imageFileSystem)
        {
            _imageRepository = imageRepository;
            _imageFileSystem = imageFileSystem;
        }

        public async Task<Result<Domain.ImageNS.Image, BaseError>> UploadAsync(
            UploadImageRequest uploadImageRequest
        )
        {
            var imageFile = uploadImageRequest.ImageFile;

            var imageNameResult = ImageName.Create(imageFile.Name);
            if (imageNameResult.IsFailure)
                return imageNameResult.Error;

            var imagePathResult = ImagePath.Create(imageFile.FileName);
            if (imagePathResult.IsFailure)
                return imagePathResult.Error;

            var userId = UserId.Create(uploadImageRequest.UserId);

            var image = Domain.ImageNS.Image.Create(
                imageNameResult.Value,
                imagePathResult.Value,
                userId
            );
            if (image.IsFailure)
                return image.Error;

            //In transaction
            var createdImageResult = await _imageRepository.CreateAsync(image.Value);
            if (createdImageResult.IsFailure)
                return createdImageResult.Error;

            await _imageFileSystem.SaveAsync(uploadImageRequest.ImageFile);

            return Result.Success<Domain.ImageNS.Image, BaseError>(createdImageResult.Value);
        }

        public async Task<Result<Domain.ImageNS.Image, BaseError>> GetByIdAsync(ImageId imageId) =>
            await _imageRepository.GetByIdAsync(imageId);

        public async Task<Result<List<Domain.ImageNS.Image>, BaseError>> GetAllAsync() =>
            await _imageRepository.GetAllAsync();

        public async Task<Result<Domain.ImageNS.Image, BaseError>> DeleteAsync(ImageId imageId) =>
            await _imageRepository.GetByIdAsync(imageId).Bind(x => _imageRepository.DeleteAsync(x));
    }
}
