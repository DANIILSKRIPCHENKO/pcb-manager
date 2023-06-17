namespace CSharpExtensions.Result
{
    public interface IResult<T>
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        T Value { get; }

        string Error { get; }
    }
}
