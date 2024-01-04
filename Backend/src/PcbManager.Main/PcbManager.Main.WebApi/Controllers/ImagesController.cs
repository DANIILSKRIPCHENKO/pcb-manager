using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Main.App.Image;
using PcbManager.Main.Domain.ImageNS.ValueObjects;
using PcbManager.Main.WebApi.Customization;
using PcbManager.Main.WebApi.Dtos;
using PcbManager.Main.WebApi.ErrorHandler;
using PcbManager.Main.WebApi.Security;

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
    [Authorize(Policies.ImageRead, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<List<ImageDto>>> GetAll() =>
        await _imageAppService
            .GetAllAsync()
            .Match(images => Ok(images.Select(ImageDto.From)), ErrorMapper.Map);

    [HttpGet("{id:guid}")]
    [Authorize(Policies.ImageRead, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<ImageDto>> GetById(Guid id) =>
        await _imageAppService
            .GetByIdAsync(ImageId.Create(id))
            .Match(image => Ok(ImageDto.From(image)), ErrorMapper.Map);

    [HttpPost]
    [Authorize(Policies.ImageWrite, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<ImageDto>> Upload(
        [FromForm] UploadImageRequest uploadImageRequest
    ) =>
        await _imageAppService
            .UploadAsync(uploadImageRequest)
            .Match(image => Ok(ImageDto.From(image)), ErrorMapper.Map);

    [HttpDelete("{id:guid}")]
    [Authorize(Policies.ImageWrite, AuthenticationSchemes = "Bearer,ApiKey")]
    public async Task<ActionResult<ImageDto>> Delete(Guid id) =>
        await _imageAppService
            .DeleteAsync(ImageId.Create(id))
            .Match(image => Ok(ImageDto.From(image)), ErrorMapper.Map);
}
