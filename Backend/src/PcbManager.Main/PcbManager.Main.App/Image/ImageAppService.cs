using CSharpFunctionalExtensions;
using PcbManager.Main.App.Abstractions;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.Domain.UserNS;
using PcbManager.Main.Domain.UserNS.ValueObjects;
using System.Transactions;

namespace PcbManager.Main.App.Image
{
    public class ImageAppService : IImageAppService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IFileSystem _fileSystem;
        private readonly ITransactionManager _transactionManager;

        public ImageAppService(
            IImageRepository imageRepository,
            IFileSystem fileSystem,
            ITransactionManager transactionManager
        )
        {
            _imageRepository = imageRepository;
            _fileSystem = fileSystem;
            _transactionManager = transactionManager;
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

            var createdImageResult = await _transactionManager.ExecuteInTransactionAsync(
                async () =>
                    await _imageRepository
                        .CreateAsync(image.Value)
                        .Tap(
                            image =>
                                _fileSystem.SaveFileAsync(
                                    $"{userId.Value}/Images",
                                    $"{image.Id.Value}.jpg",
                                    uploadImageRequest.ImageFile.OpenReadStream()
                                )
                        )
            );

            return createdImageResult;
        }

        public async Task<Result<Domain.ImageNS.Image, BaseError>> GetByIdAsync(ImageId imageId) =>
            await _imageRepository.GetByIdAsync(imageId);

        public async Task<Result<List<Domain.ImageNS.Image>, BaseError>> GetAllAsync() =>
            await _imageRepository.GetAllAsync();

        public async Task<Result<Domain.ImageNS.Image, BaseError>> DeleteAsync(ImageId imageId) =>
            await _transactionManager.ExecuteInTransactionAsync(
                async () =>
                    await _imageRepository
                        .GetByIdAsync(imageId)
                        .Bind(image => _imageRepository.DeleteAsync(image))
                        .Tap(
                            image =>
                                _fileSystem.DeleteFile(
                                    $"{image.UserId.Value}/Images",
                                    $"{image.Id.Value}.jpg"
                                )
                        )
            );
    }
}
