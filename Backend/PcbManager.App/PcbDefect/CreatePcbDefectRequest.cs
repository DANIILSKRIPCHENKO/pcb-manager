using PcbManager.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.App.PcbDefect;

public class CreatePcbDefectRequest
{
    public PcbDefectTypeEnum PcbDefectTypeEnum { get; }

    public Guid ReportId { get; }
}