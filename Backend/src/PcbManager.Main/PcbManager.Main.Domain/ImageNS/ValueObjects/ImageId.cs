using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.Domain.ImageNS.ValueObjects
{
    public class ImageId
    {
        public Guid Value { get; }

        private ImageId(Guid value)
        {
            Value = value;
        }

        public static Result<ImageId, BaseError> Create(Guid value) =>
            Result.Success<ImageId, BaseError>(new ImageId(value));

        public static Result<ImageId, BaseError> CreateUnique() =>
            Result.Success<ImageId, BaseError>(new ImageId(Guid.NewGuid()));
    }
}
