using CSharpFunctionalExtensions;
using PcbManager.Main.App.Abstractions;
using PcbManager.Main.App.Image;
using PcbManager.Main.App.User;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.Domain.ReportNS.ValueObjects;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.App.Report;

public class ReportAppService : IReportAppService
{
    private readonly IReportRepository _reportRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IFileSystem _fileSystem;
    private readonly IReportGeneratorAdapter _reportGeneratorAdapter;
    private readonly ITransactionManager _transactionManager;
    private readonly IUserRepository _userReporitory;

    public ReportAppService(
        IReportRepository reportRepository,
        IImageRepository imageRepository,
        IUserRepository userRepository,
        IFileSystem fileSystem,
        IReportGeneratorAdapter reportGeneratorAdapter,
        ITransactionManager transactionManager
    )
    {
        _reportRepository = reportRepository;
        _imageRepository = imageRepository;
        _fileSystem = fileSystem;
        _reportGeneratorAdapter = reportGeneratorAdapter;
        _transactionManager = transactionManager;
        _userReporitory = userRepository;
    }

    public async Task<Result<List<Domain.ReportNS.Report>, BaseError>> GetAllAsync() =>
        await _reportRepository.GetAllAsync();

    public async Task<Result<Domain.ReportNS.Report, BaseError>> GetByIdAsync(ReportId id) =>
        await _reportRepository.GetByIdAsync(id);

    public async Task<Result<Domain.ReportNS.Report, BaseError>> CreateAsync(
        CreateReportRequest createReportRequest
    )
    {
        var imageResult = await _imageRepository.GetByIdAsync(
            ImageId.Create(createReportRequest.ImageId)
        );

        var userResult = await _userReporitory.GetByIdAsync(
            UserId.Create(createReportRequest.UserId)
        );

        if (userResult.IsFailure)
            return userResult.Error;

        var reportStreamResult = await imageResult
            .Map(
                image =>
                    _fileSystem.GetFileSteram(
                        $"{image.UserId.Value}/Images",
                        $"{image.Id.Value}.jpg"
                    )
            )
            .Map(imageStream => _reportGeneratorAdapter.GenerateReportAsync(imageStream));

        return await _transactionManager.ExecuteInTransactionAsync(
            async () =>
                await imageResult
                    .Bind(image => Domain.ReportNS.Report.Create(image.Id))
                    .Bind(report => _reportRepository.CreateAsync(report))
                    .Tap(
                        report =>
                            _fileSystem.SaveFileAsync(
                                $"{userResult.Value.Id.Value}/Reports",
                                $"{report.Id.Value}.jpg",
                                reportStreamResult.Value
                            )
                    )
        );
    }

    public async Task<Result<Domain.ReportNS.Report, BaseError>> DeleteAsync(ReportId id)
    {
        var reportResult = await _reportRepository.GetByIdAsync(id);
        if (reportResult.IsFailure)
            return reportResult.Error;

        var userResult = await _imageRepository
            .GetByIdAsync(reportResult.Value.ImageId)
            .Bind(image => _userReporitory.GetByIdAsync(image.UserId));
        if (userResult.IsFailure)
            return userResult.Error;

        return await _transactionManager.ExecuteInTransactionAsync(
            async () =>
                await _reportRepository
                    .DeleteAsync(reportResult.Value)
                    .Tap(
                        report =>
                            _fileSystem.DeleteFile(
                                $"{userResult.Value.Id.Value}/Reports",
                                $"{report.Id.Value}.jpg"
                            )
                    )
        );
    }
}
