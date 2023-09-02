using PcbManager.Domain.Common;

namespace PcbManager.Domain.Abstractions;

public interface IUpdatedAtEntity
{
    public UpdatedAt UpdatedAt { get; }
}