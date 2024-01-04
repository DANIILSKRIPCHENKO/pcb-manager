namespace PcbManager.Main.Domain.ImageNS.ValueObjects
{
    public class ImageId
    {
        public Guid Value { get; }

        private ImageId(Guid value)
        {
            Value = value;
        }

        public static ImageId Create(Guid value) => new(value);

        public static ImageId CreateUnique() => new(Guid.NewGuid());
    }
}
