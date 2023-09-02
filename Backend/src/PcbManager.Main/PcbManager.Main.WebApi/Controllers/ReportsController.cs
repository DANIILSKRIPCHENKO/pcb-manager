using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Main.App.Report;
using PcbManager.Main.Domain.ReportNS.ValueObjects;
using PcbManager.Main.WebApi.Customization;
using PcbManager.Main.WebApi.Dtos;
using PcbManager.Main.WebApi.ErrorHandler;

namespace PcbManager.Main.WebApi.Controllers;

[ApiController]
[StandardRoute]
public class ReportsController : ControllerBase
{
    private readonly IReportAppService _reportAppService;

    public ReportsController(IReportAppService reportAppService)
    {
        _reportAppService = reportAppService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReportDto>>> GetAll() =>
        await _reportAppService.GetAllAsync().Match(reports =>
            Ok(reports.Select(ReportDto.From)), ErrorMapper.Map);

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReportDto>> GetById(Guid id) =>
        await _reportAppService.GetByIdAsync(ReportId.Create(id).Value)
            .Match(report => Ok(ReportDto.From(report)), ErrorMapper.Map);

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateReportRequest createReportRequest) =>
        await _reportAppService.CreateAsync(createReportRequest)
            .Match(report => Ok(ReportDto.From(report)), ErrorMapper.Map);

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<UserDto>> Delete(Guid id) =>
        await _reportAppService.DeleteAsync(ReportId.Create(id).Value)
            .Match(report => Ok(ReportDto.From(report)), ErrorMapper.Map);
}