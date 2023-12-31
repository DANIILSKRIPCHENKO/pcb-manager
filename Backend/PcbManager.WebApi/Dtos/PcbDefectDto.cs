using PcbManager.Domain.PcbDefectNS;
using PcbManager.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.WebApi.Dtos;

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