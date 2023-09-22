using PcbManager.Main.Domain.Common;

namespace PcbManager.Main.Domain.Abstractions;

public interface ICreatedAtEntity
{
    public CreatedAt CreatedAt { get; }
}
