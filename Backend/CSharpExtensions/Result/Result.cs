namespace CSharpExtensions.Result
{
    public readonly struct Result<T> : IResult<T>
    {
        private readonly string _error = string.Empty;

        private readonly bool _isErrorSet = false;

        private readonly T _value;

        private Result(T value)
        {
            _value = value;
        }

        private Result(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new Exception("Error cannot be null or empty");

            _error = error;
            _isErrorSet = true;
            _value = default;
        }

        public bool IsFailure => _isErrorSet;

        public bool IsSuccess => !IsFailure;

        public T Value => GetValueWithErrorGuard();

        public string Error => GetErrorWithSuccessGuard();

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(error);
        }

        private T GetValueWithErrorGuard()
        {
            if (IsFailure)
                throw new Exception();

            return _value;
        }

        private string GetErrorWithSuccessGuard()
        {
            if (IsSuccess)
                throw new Exception();

            return _error;
        }
    }
}
