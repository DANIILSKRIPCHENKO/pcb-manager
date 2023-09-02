using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.ImageNS.ValueObjects
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
