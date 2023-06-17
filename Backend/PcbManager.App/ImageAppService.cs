using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.ImageNS.ValueObjects;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.App
{
    public class ImageAppService : IImageAppService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageFileSystem _imageFileSystem;
        private readonly IUserAppService _userAppService;

        public ImageAppService(
            IImageRepository imageRepository,
            IImageFileSystem imageFileSystem,
            IUserAppService userAppService)
        {
            _imageRepository = imageRepository;
            _imageFileSystem = imageFileSystem;
            _userAppService = userAppService;
        }

        public async Task<Result<Image>> UploadAsync(UploadImageRequest uploadImageRequest)
        {
            var imageFile = uploadImageRequest.ImageFile;

            var imageName = ImageName.Create(imageFile.Name);
            if (imageName.IsFailure)
                return Result<Image>.Failure(imageName.Error);

            var imagePath = ImagePath.Create(imageFile.FileName);
            if (imagePath.IsFailure)
                return Result<Image>.Failure(imagePath.Error);

            var userId = UserId.Create(uploadImageRequest.UserId);
            if (userId.IsFailure)
                return Result<Image>.Failure(userId.Error);

            var user = await _userAppService.GetByIdAsync(userId.Value);
            if(user.HasNoValue)
                return Result<Image>.Failure("User with such id does not exist");

            var image = Image.Create(imageName.Value, imagePath.Value, user.Value);
            if(image.IsFailure)
                return Result<Image>.Failure(image.Error);

            //In transaction
            var iamgeResult = await _imageRepository.CreateAsync(image.Value);
            if (iamgeResult.IsFailure)
                return Result<Image>.Failure(iamgeResult.Error);

            await _imageFileSystem.SaveAsync(uploadImageRequest.ImageFile);

            return Result<Image>.Success(iamgeResult.Value);
        }

        public async Task<Maybe<Image>> GetByIdAsync(ImageId imageId)
        {
            return await _imageRepository.GetByIdAsync(imageId);
        }

        public Task<Maybe<List<Image>>> GetAllAsync()
        {
            return _imageRepository.GetAllAsync();
        }
    }
}
