using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.App
{
    public interface ITransactionManager
    {
        public UnitResult<BaseError> ExecuteInTransaction(Action action);

        public Task<UnitResult<BaseError>> ExecuteInTransactionAsync<T>(Func<Task> action);

        public Task<Result<T, BaseError>> ExecuteInTransactionAsync<T>(
            Func<Task<Result<T, BaseError>>> action
        );
    }
}
