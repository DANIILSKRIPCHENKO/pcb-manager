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
        return entities;
    }

    public async Task<Result<TEntity, BaseError>> GetByIdAsync(TId id)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return new EntityNotFoundError();
        return entity;
    }

    public async Task<Result<TEntity, BaseError>> CreateAsync(TEntity entity)
    {
        var entityEntry = await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<Result<TEntity, BaseError>> DeleteAsync(TEntity entity)
    {
        var removedEntity = _context.Remove(entity);
        await _context.SaveChangesAsync();
        return removedEntity.Entity;
    }

    public async Task<Result<TEntity, BaseError>> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _context.Update(entity);
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }
}
