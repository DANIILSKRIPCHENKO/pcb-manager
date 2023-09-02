using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors;

namespace PcbManager.Main.Domain.ImageNS.ValueObjects
{
    public class ImagePath
    {
        public string Value { get; }

        private ImagePath(string value)
        {
            Value = value;
        }

        public static IResult<ImagePath, ValidationError> Create(string value) =>
            string.IsNullOrWhiteSpace(value)
                ? Result.Failure<ImagePath, ValidationError>(new ValidationError())
                : Result.Success<ImagePath, ValidationError>(new ImagePath(value));
    }
}
