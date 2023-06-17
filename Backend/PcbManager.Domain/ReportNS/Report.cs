using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.ImageNS.ValueObjects;
using PcbManager.Domain.PcbDefectNS;
using PcbManager.Domain.ReportNS.ValueObjects;
using PcbManager.Domain.UserNS;

namespace PcbManager.Domain.ReportNS;

public class Report
{
    private readonly List<PcbDefect> _pcbDefect = new();

    private Report(Image image, List<PcbDefect> pcbDefects)
    {
        Id = ReportId.CreateUnique().Value;
        Image = image;
        _pcbDefect = pcbDefects;
    }

    private Report(){}

    public ReportId Id { get; }

    public ImageId ImageId { get; }
    public Image Image { get; }

    public IReadOnlyList<PcbDefect> PcbDefects => _pcbDefect.AsReadOnly();

    public static Result<Report> Create(Image image, List<PcbDefect> pcbDefects)
    {
        return Result<Report>.Success(new Report(image, pcbDefects));
    }
}