namespace PcbManager.Main.Domain.UserNS.ValueObjects
{
    public class UserId
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId Create(Guid value) => new(value);

        public static UserId CreateUnique() => new(Guid.NewGuid());
    }
}
