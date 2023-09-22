using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Main.App.PcbDefect;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;
using PcbManager.Main.WebApi.Customization;
using PcbManager.Main.WebApi.Dtos;
using PcbManager.Main.WebApi.ErrorHandler;
using PcbManager.Main.WebApi.Security;

namespace PcbManager.Main.WebApi.Controllers;

[ApiController]
[StandardRoute]
public class PcbDefectsController : ControllerBase
{
    private readonly IPcbDefectAppService _pcbDefectAppService;

    public PcbDefectsController(IPcbDefectAppService pcbDefectAppService)
    {
        _pcbDefectAppService = pcbDefectAppService;
    }

    [HttpGet]
    [Authorize(Policies.ReportRead, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<List<PcbDefectDto>>> GetAll() =>
        await _pcbDefectAppService
            .GetAllAsync()
            .Match(pcbDefects => Ok(pcbDefects.Select(PcbDefectDto.From)), ErrorMapper.Map);

    [HttpGet("{id:guid}")]
    [Authorize(Policies.ReportRead, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<PcbDefectDto>> GetById(Guid id) =>
        await _pcbDefectAppService
            .GetByIdAsync(PcbDefectId.Create(id).Value)
            .Match(pcbDefect => Ok(PcbDefectDto.From(pcbDefect)), ErrorMapper.Map);

    [HttpPost]
    [Authorize(Policies.ReportWrite, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<UserDto>> Create(
        [FromBody] CreatePcbDefectRequest createPcbDefectRequest
    ) =>
        await _pcbDefectAppService
            .CreateAsync(createPcbDefectRequest)
            .Match(pcbDefect => Ok(PcbDefectDto.From(pcbDefect)), ErrorMapper.Map);

    [HttpDelete("{id:guid}")]
    [Authorize(Policies.ReportWrite, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<PcbDefectDto>> Delete(Guid id) =>
        await _pcbDefectAppService
            .DeleteAsync(PcbDefectId.Create(id).Value)
            .Match(pcbDefect => Ok(PcbDefectDto.From(pcbDefect)), ErrorMapper.Map);
}
