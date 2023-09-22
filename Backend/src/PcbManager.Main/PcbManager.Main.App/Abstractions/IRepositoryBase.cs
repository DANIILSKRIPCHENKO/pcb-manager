using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Abstractions;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.App.Abstractions;

public interface IRepositoryBase<TEntity, in TId>
    where TEntity : class, IIdEntity<TId>
    where TId : class
{
    public Task<Result<TEntity, BaseError>> CreateAsync(TEntity entity);

    public Task<Result<TEntity, BaseError>> GetByIdAsync(TId id);

    public Task<Result<List<TEntity>, BaseError>> GetAllAsync();

    public Task<Result<TEntity, BaseError>> DeleteAsync(TEntity entity);

    public Task<Result<TEntity, BaseError>> UpdateAsync(TEntity entity);
}
