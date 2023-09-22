using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.Main.App.PcbDefect;

public class CreatePcbDefectRequest
{
    public PcbDefectTypeEnum PcbDefectTypeEnum { get; }

    public Guid ReportId { get; }
}
