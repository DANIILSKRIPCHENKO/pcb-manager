using PcbManager.Main.App.PcbDefect;
using PcbManager.Main.DAL.Abstractions;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.Main.DAL.PcbDefect;

public class PcbDefectRepository
    : RepositoryBase<Domain.PcbDefectNS.PcbDefect, PcbDefectId>,
        IPcbDefectRepository
{
    public PcbDefectRepository(PcbManagerDbContext context)
        : base(context) { }
}
