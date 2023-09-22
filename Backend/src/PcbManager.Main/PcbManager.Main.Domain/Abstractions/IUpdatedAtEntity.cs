using PcbManager.Main.Domain.Common;

namespace PcbManager.Main.Domain.Abstractions;

public interface IUpdatedAtEntity
{
    public UpdatedAt UpdatedAt { get; }
}
