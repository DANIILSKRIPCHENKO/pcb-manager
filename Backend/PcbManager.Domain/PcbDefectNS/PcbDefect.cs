using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.PcbDefectNS.ValueObjects;
using PcbManager.Domain.ReportNS;

namespace PcbManager.Domain.PcbDefectNS;

public class PcbDefect
{
    public PcbDefect(Report report)
    {
        Id = PcbDefectId.CreateUnique().Value;
        Report = report;
    }

    public PcbDefect() {}

    public PcbDefectId Id { get; }

    public PcbDefectType PcbDefectType { get; }

    public Report Report { get; }


    public static Result<PcbDefect> Create(Report report)
    {
        return Result<PcbDefect>.Success(new PcbDefect(report));
    }
}