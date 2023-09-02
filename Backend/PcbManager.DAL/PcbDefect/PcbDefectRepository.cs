using PcbManager.App.PcbDefect;
using PcbManager.DAL.Abstractions;
using PcbManager.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.DAL.PcbDefect;

public class PcbDefectRepository : RepositoryBase<Domain.PcbDefectNS.PcbDefect, PcbDefectId>, IPcbDefectRepository
{
    public PcbDefectRepository(PcbManagerDbContext context) : base(context)
    {

    }
}