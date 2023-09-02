using CSharpFunctionalExtensions;
using PcbManager.Domain.Abstractions;
using PcbManager.Domain.Common;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.ImageNS.ValueObjects;
using PcbManager.Domain.PcbDefectNS;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.Domain.ReportNS;

public class Report : IIdEntity<ReportId>, ICreatedAtEntity
{
    private readonly List<PcbDefect> _pcbDefect = new();

    private Report(ImageId imageId)
    {
        Id = ReportId.CreateUnique().Value;
        ImageId = imageId;
        CreatedAt = CreatedAt.FromNow().Value;
    }

    private Report(){}

    public ReportId Id { get; }

    public ImageId ImageId { get; }

    public CreatedAt CreatedAt { get; }

    public static Result<Report, BaseError> Create(ImageId imageId) =>
        Result.Success<Report, BaseError>(new Report(imageId));
}