using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Main.App.Image;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.WebApi.Customization;
using PcbManager.Main.WebApi.Dtos;
using PcbManager.Main.WebApi.ErrorHandler;

namespace PcbManager.Main.WebApi.Controllers;

[ApiController]
[StandardRoute]
public class ImagesController : ControllerBase
{
    private readonly IImageAppService _imageAppService;

    public ImagesController(IImageAppService imageAppService)
    {
        _imageAppService = imageAppService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ImageDto>>> GetAll() =>
        await _imageAppService.GetAllAsync()
            .Match(images => Ok(images.Select(ImageDto.From)), ErrorMapper.Map);

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ImageDto>> GetById(Guid id) =>
    await _imageAppService.GetByIdAsync(ImageId.Create(id).Value)
        .Match(image => Ok(ImageDto.From(image)), ErrorMapper.Map);

    [HttpPost]
    public async Task<ActionResult<ImageDto>> Upload([FromForm] UploadImageRequest uploadImageRequest) =>
        await _imageAppService.UploadAsync(uploadImageRequest)
            .Match(image => Ok(ImageDto.From(image)), ErrorMapper.Map);

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ImageDto>> Delete(Guid id) =>
        await _imageAppService.DeleteAsync(ImageId.Create(id).Value)
            .Match(image => Ok(ImageDto.From(image)), ErrorMapper.Map);
}