using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS.ValueObjects;
using PcbManager.Domain.ReportNS;
using PcbManager.Domain.UserNS;

namespace PcbManager.Domain.ImageNS
{
    public class Image
    {
        private Image(ImageName imageName, ImagePath imagePath, User user)
        {
            Id = ImageId.CreateUnique().Value;
            ImageName = imageName;
            ImagePath = imagePath;
            User= user;
        }

        private Image()
        {
        }

        public ImageId Id { get; }

        public ImageName ImageName { get; }

        public ImagePath ImagePath { get; }

        public User User { get; }

        public Report Report { get; }

        public static Result<Image> Create(ImageName imageName, ImagePath imagePath, User user)
        {
            return Result<Image>.Success(new Image(imageName, imagePath, user));
        }
    }
}