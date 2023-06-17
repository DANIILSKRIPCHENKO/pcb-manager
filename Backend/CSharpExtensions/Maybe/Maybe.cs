namespace CSharpExtensions.Maybe
{
    public readonly struct Maybe<T> : IMaybe<T>
    {
        private readonly bool _isValueSet;

        private readonly T _value;

        private Maybe(T value)
        {
            if (value == null)
            {
                _isValueSet = false;
                _value = default;
                return;
            }

            _isValueSet = true;
            _value = value;
        }

        public T Value => GetValueOrThrow();

        public bool HasValue => _isValueSet;

        public bool HasNoValue => !_isValueSet;

        private T GetValueOrThrow()
        {
            if (HasNoValue)
                throw new ArgumentNullException(nameof(_value));

            return _value;
        }

        public static Maybe<T> From<T>(T obj)
        {
            return new Maybe<T>(obj);
        }
    }
}