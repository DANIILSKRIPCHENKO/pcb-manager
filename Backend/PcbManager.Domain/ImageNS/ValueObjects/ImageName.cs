using CSharpExtensions.Result;

namespace PcbManager.Domain.ImageNS.ValueObjects
{
    public class ImageName
    {
        public string Value { get; }

        private ImageName(string value)
        {
            Value = value;
        }

        public static Result<ImageName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<ImageName>.Failure("Image name is invalid");

            return Result<ImageName>.Success(new ImageName(value));
        }
    }
}
