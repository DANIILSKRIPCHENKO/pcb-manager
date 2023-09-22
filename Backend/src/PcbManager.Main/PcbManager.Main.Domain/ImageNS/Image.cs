using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Abstractions;
using PcbManager.Main.Domain.Common;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.Domain.ImageNS
{
    public class Image : IIdEntity<ImageId>, ICreatedAtEntity
    {
        private Image(ImageName imageName, ImagePath imagePath, UserId userId)
        {
            Id = ImageId.CreateUnique().Value;
            ImageName = imageName;
            ImagePath = imagePath;
            UserId = userId;
            CreatedAt = CreatedAt.FromNow().Value;
        }

#pragma warning disable CS8618
        private Image() { }
#pragma warning restore CS8618

        public ImageId Id { get; }

        public ImageName ImageName { get; }

        public ImagePath ImagePath { get; }

        public UserId UserId { get; }

        public CreatedAt CreatedAt { get; }

        public static IResult<Image, BaseError> Create(
            ImageName imageName,
            ImagePath imagePath,
            UserId userId
        ) => Result.Success<Image, BaseError>(new Image(imageName, imagePath, userId));
    }
}
