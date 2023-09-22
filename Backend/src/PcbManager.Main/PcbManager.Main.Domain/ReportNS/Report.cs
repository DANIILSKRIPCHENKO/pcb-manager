using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Abstractions;
using PcbManager.Main.Domain.Common;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.Domain.PcbDefectNS;
using PcbManager.Main.Domain.ReportNS.ValueObjects;

namespace PcbManager.Main.Domain.ReportNS;

public class Report : IIdEntity<ReportId>, ICreatedAtEntity
{
    private readonly List<PcbDefect> _pcbDefect = new();

    private Report(ImageId imageId)
    {
        Id = ReportId.CreateUnique().Value;
        ImageId = imageId;
        CreatedAt = CreatedAt.FromNow().Value;
    }

    private Report() { }

    public ReportId Id { get; }

    public ImageId ImageId { get; }

    public CreatedAt CreatedAt { get; }

    public static Result<Report, BaseError> Create(ImageId imageId) =>
        Result.Success<Report, BaseError>(new Report(imageId));
}
