using CSharpFunctionalExtensions;
using PcbManager.Domain.Abstractions;
using PcbManager.Domain.Common;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.PcbDefectNS.ValueObjects;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.Domain.PcbDefectNS;

public class PcbDefect : IIdEntity<PcbDefectId>, ICreatedAtEntity
{
    private PcbDefect(PcbDefectType pcbDefectType, ReportId reportId)
    {
        Id = PcbDefectId.CreateUnique().Value;
        PcbDefectType = pcbDefectType;
        ReportId = reportId;
        CreatedAt = CreatedAt.FromNow().Value;
    }

    private PcbDefect() {}

    public PcbDefectId Id { get; }

    public PcbDefectType PcbDefectType { get; }

    public ReportId ReportId { get; }

    public CreatedAt CreatedAt { get; }

    public static Result<PcbDefect, BaseError> Create(PcbDefectType pcbDefectType, ReportId reportId) =>
        Result.Success<PcbDefect, BaseError>(new PcbDefect(pcbDefectType, reportId));
}