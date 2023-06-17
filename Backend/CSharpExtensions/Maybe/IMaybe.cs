namespace CSharpExtensions.Maybe
{
    public interface IMaybe<T>
    {
        T Value { get; }
        bool HasValue { get; }
        bool HasNoValue { get; }
    }
}
