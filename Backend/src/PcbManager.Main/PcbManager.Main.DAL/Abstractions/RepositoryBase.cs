using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PcbManager.Main.App.Abstractions;
using PcbManager.Main.Domain.Abstractions;
using PcbManager.Main.Domain.Errors;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.DAL.Abstractions;

public abstract class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
    where TEntity : class, IIdEntity<TId>
    where TId : class
{
    private readonly DbContext _context;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TEntity>, BaseError>> GetAllAsync()
    {
        var entities = await _context.Set<TEntity>().ToListAsync();
        return Result.Success<List<TEntity>, BaseError>(entities);
    }

    public async Task<Result<TEntity, BaseError>> GetByIdAsync(TId id)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstAsync(x => x.Id == id);
            return Result.Success<TEntity, BaseError>(entity);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure<TEntity, BaseError>(new EntityNotFoundError());
        }
    }

    public async Task<Result<TEntity, BaseError>> CreateAsync(TEntity entity)
    {
        try
        {
            var entityEntry = await _context.Set<TEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return Result.Success<TEntity, BaseError>(entityEntry.Entity);
        }
        catch (DbUpdateException ex)
        {
            return Result.Failure<TEntity, BaseError>(new ConflictError());
        }
    }

    public async Task<Result<TEntity, BaseError>> DeleteAsync(TEntity entity)
    {
        try
        {
            var removedEntity = _context.Remove(entity);
            await _context.SaveChangesAsync();

            return Result.Success<TEntity, BaseError>(removedEntity.Entity);
        }
        catch (DbUpdateException ex)
        {
            return Result.Failure<TEntity, BaseError>(new ConflictError());
        }
    }

    public async Task<Result<TEntity, BaseError>> UpdateAsync(TEntity entity)
    {
        try
        {
            var updatedEntity = _context.Update(entity);
            await _context.SaveChangesAsync();

            return Result.Success<TEntity, BaseError>(updatedEntity.Entity);
        }
        catch (DbUpdateException ex)
        {
            return Result.Failure<TEntity, BaseError>(new ConflictError());
        }
    }
}