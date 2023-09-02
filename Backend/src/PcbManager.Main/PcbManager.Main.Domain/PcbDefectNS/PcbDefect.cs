using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Abstractions;
using PcbManager.Main.Domain.Common;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;
using PcbManager.Main.Domain.ReportNS.ValueObjects;

namespace PcbManager.Main.Domain.PcbDefectNS;

public class PcbDefect : IIdEntity<PcbDefectId>, ICreatedAtEntity
{
    private PcbDefect(PcbDefectType pcbDefectType, ReportId reportId)
    {
        Id = PcbDefectId.CreateUnique().Value;
        PcbDefectType = pcbDefectType;
        ReportId = reportId;
        CreatedAt = CreatedAt.FromNow().Value;
    }

    private PcbDefect() { }

    public PcbDefectId Id { get; }

    public PcbDefectType PcbDefectType { get; }

    public ReportId ReportId { get; }

    public CreatedAt CreatedAt { get; }

    public static Result<PcbDefect, BaseError> Create(PcbDefectType pcbDefectType, ReportId reportId) =>
        Result.Success<PcbDefect, BaseError>(new PcbDefect(pcbDefectType, reportId));
}