namespace PcbManager.Domain.Abstractions;

public interface IIdEntity<out TId> where TId : class
{
    public TId Id { get; }
}