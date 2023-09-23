using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using PcbManager.Main.App;
using PcbManager.Main.Domain.Errors;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.DAL
{
    public class TransactionManager : ITransactionManager
    {
        private readonly PcbManagerDbContext _context;

        public TransactionManager(PcbManagerDbContext context)
        {
            _context = context;
        }

        public UnitResult<BaseError> ExecuteInTransaction(Action action)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                action.Invoke();
                transaction.Commit();
                return UnitResult.Success<BaseError>();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return UnitResult.Failure(new TransactionError() as BaseError);
            }
        }

        public async Task<UnitResult<BaseError>> ExecuteInTransactionAsync<T>(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await action.Invoke();
                transaction.Commit();
                return UnitResult.Success<BaseError>();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return UnitResult.Failure(new TransactionError() as BaseError);
            }
        }

        public async Task<Result<T, BaseError>> ExecuteInTransactionAsync<T>(Func<Task<Result<T, BaseError>>> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await action.Invoke();
                if (result.IsFailure)
                {
                    await transaction.RollbackAsync();
                    return new TransactionError();
                }
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new TransactionError();
            }
        }
    }
}
