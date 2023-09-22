using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors;

namespace PcbManager.Main.Domain.ImageNS.ValueObjects
{
    public class ImageName
    {
        public string Value { get; }

        private ImageName(string value)
        {
            Value = value;
        }

        public static IResult<ImageName, ValidationError> Create(string value) =>
            string.IsNullOrWhiteSpace(value)
                ? Result.Failure<ImageName, ValidationError>(new ValidationError())
                : Result.Success<ImageName, ValidationError>(new ImageName(value));
    }
}
