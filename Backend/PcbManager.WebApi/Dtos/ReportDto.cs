using PcbManager.Domain.ReportNS;

namespace PcbManager.WebApi.Dtos;

public class ReportDto
{
    private ReportDto(Report report)
    {
        Id = report.Id.Value;
        ImageId = report.Id.Value;
    }

    public Guid Id { get; }

    public Guid ImageId { get; }

    public static ReportDto From(Report report) => new(report);
}