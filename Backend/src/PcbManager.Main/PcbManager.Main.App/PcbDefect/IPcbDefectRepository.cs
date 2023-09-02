using PcbManager.Main.App.Abstractions;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.Main.App.PcbDefect;

public interface IPcbDefectRepository : IRepositoryBase<Domain.PcbDefectNS.PcbDefect, PcbDefectId>
{

}