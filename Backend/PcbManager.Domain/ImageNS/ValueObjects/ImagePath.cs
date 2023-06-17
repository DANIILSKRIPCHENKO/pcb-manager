using CSharpExtensions.Result;

namespace PcbManager.Domain.ImageNS.ValueObjects
{
    public class ImagePath
    {
        public string Value { get; }

        private ImagePath(string value)
        {
            Value = value;
        }

        public static Result<ImagePath> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<ImagePath>.Failure("Invalid image path");

            return Result<ImagePath>.Success(new ImagePath(value));
        }
    }
}
