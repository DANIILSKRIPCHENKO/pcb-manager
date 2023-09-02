using PcbManager.Domain.Common;

namespace PcbManager.Domain.Abstractions;

public interface ICreatedAtEntity
{
    public CreatedAt CreatedAt { get; }
}