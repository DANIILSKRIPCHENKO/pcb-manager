using PcbManager.Main.Domain.PcbDefectNS;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.Main.WebApi.Dtos;

public class PcbDefectDto
{
    private PcbDefectDto(PcbDefect pcbDefect)
    {
        Id = pcbDefect.Id.Value;
        PcbDefectType = pcbDefect.PcbDefectType.Value;
        ReportId = pcbDefect.ReportId.Value;
    }

    public Guid Id { get; }

    public PcbDefectTypeEnum PcbDefectType { get; }

    public Guid ReportId { get; }

    public static PcbDefectDto From(PcbDefect pcbDefect) => new(pcbDefect);
}