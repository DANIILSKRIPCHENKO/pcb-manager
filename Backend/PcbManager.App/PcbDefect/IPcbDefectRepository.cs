using PcbManager.App.Abstractions;
using PcbManager.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.App.PcbDefect;

public interface IPcbDefectRepository : IRepositoryBase<Domain.PcbDefectNS.PcbDefect, PcbDefectId>
{

}