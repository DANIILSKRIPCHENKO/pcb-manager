using CSharpExtensions.Result;

namespace PcbManager.Domain.ImageNS.ValueObjects
{
    public class ImageId
    {
        public Guid Value { get; }

        private ImageId(Guid value)
        {
            Value = value;
        }

        public static Result<ImageId> Create(Guid value)
        {
            return Result<ImageId>.Success(new ImageId(value));
        }

        public static Result<ImageId> CreateUnique()
        {
            return Result<ImageId>.Success(new ImageId(Guid.NewGuid()));
        }
    }
}
