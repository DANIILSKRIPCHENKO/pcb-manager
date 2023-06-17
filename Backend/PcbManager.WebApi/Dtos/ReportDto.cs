using PcbManager.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.WebApi.Dtos;

public class ReportDto
{
    public Guid Id { get; set; }

    public Guid ImageId { get; set; }

    public List<PcbDefectTypeEnum> PcbDefectTypes { get; set; }
}