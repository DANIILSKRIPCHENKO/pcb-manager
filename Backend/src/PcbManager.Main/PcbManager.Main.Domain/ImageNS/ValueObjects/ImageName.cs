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

        public static Result<ImageName, ValidationError> Create(string value) =>
            string.IsNullOrWhiteSpace(value) ? new ValidationError() : new ImageName(value);
    }
}
