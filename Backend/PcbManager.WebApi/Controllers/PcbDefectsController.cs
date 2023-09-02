using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PcbManager.App.PcbDefect;
using PcbManager.Domain.PcbDefectNS.ValueObjects;
using PcbManager.WebApi.Customization;
using PcbManager.WebApi.Dtos;
using PcbManager.WebApi.ErrorHandler;

namespace PcbManager.WebApi.Controllers;

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
    public async Task<ActionResult<List<PcbDefectDto>>> GetAll() =>
        await _pcbDefectAppService.GetAllAsync().Match(pcbDefects =>
            Ok(pcbDefects.Select(PcbDefectDto.From)), ErrorMapper.Map);

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PcbDefectDto>> GetById(Guid id) =>
        await _pcbDefectAppService.GetByIdAsync(PcbDefectId.Create(id).Value)
            .Match(pcbDefect => Ok(PcbDefectDto.From(pcbDefect)), ErrorMapper.Map);

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreatePcbDefectRequest createPcbDefectRequest) =>
        await _pcbDefectAppService.CreateAsync(createPcbDefectRequest)
            .Match(pcbDefect => Ok(PcbDefectDto.From(pcbDefect)), ErrorMapper.Map);

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<PcbDefectDto>> Delete(Guid id) =>
        await _pcbDefectAppService.DeleteAsync(PcbDefectId.Create(id).Value)
            .Match(pcbDefect => Ok(PcbDefectDto.From(pcbDefect)), ErrorMapper.Map);
}