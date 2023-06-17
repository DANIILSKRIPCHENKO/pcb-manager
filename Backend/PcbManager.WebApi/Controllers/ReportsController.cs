using Microsoft.AspNetCore.Mvc;
using PcbManager.Domain.PcbDefectNS.ValueObjects;
using PcbManager.WebApi.Customization;
using PcbManager.WebApi.Dtos;

namespace PcbManager.WebApi.Controllers;

[ApiController]
[StandardRoute]
public class ReportsController : ControllerBase
{
    public ReportsController()
    {

    }

    [HttpGet]
    public ActionResult<List<ReportDto>> GetAllByUserId(Guid userId)
    {
        return Ok(new List<ReportDto>()
        {
            new ReportDto()
            {
                PcbDefectTypes = new List<PcbDefectTypeEnum>() {PcbDefectTypeEnum.Short},
                Id = Guid.NewGuid(),
                ImageId = Guid.NewGuid()
            }
        });
    }

    [HttpPost]
    public ActionResult<ReportDto> Create(Guid imageId)
    {
        return Ok(new ReportDto()
        {
            Id = Guid.NewGuid(),
            ImageId = imageId,
            PcbDefectTypes = new List<PcbDefectTypeEnum>() {PcbDefectTypeEnum.Short},
        });
    }
}