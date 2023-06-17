using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PcbManager.App;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.ImageNS.ValueObjects;
using PcbManager.WebApi.Customization;
using PcbManager.WebApi.Dtos;

namespace PcbManager.WebApi.Controllers
{
    [ApiController]
    [StandardRoute]
    public class ImagesController : ControllerBase
    {
        private readonly IImageAppService _imageAppService;
        private readonly IMapper _mapper;

        public ImagesController(IImageAppService imageAppService, IMapper mapper)
        {
            _imageAppService = imageAppService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Image>> GetById(Guid id)
        {
            var maybeImage = await _imageAppService.GetByIdAsync(ImageId.Create(id).Value);

            if (maybeImage.HasNoValue)
                return NotFound();

            return Ok(_mapper.Map<ImageDto>(maybeImage.Value));
        }

        [HttpGet]
        public async Task<ActionResult<Image>> GetAll(Guid userId)
        {
            var maybeImages = await _imageAppService.GetAllAsync();

            if (maybeImages.HasNoValue)
                return NotFound();

            return Ok(_mapper.Map<List<ImageDto>>(maybeImages.Value));
        }

        [HttpPost]
        public async Task<ActionResult<Image>> Upload([FromForm] UploadImageRequest uploadImageRequest)
        {
            var imageResult = await _imageAppService.UploadAsync(uploadImageRequest);

            if (imageResult.IsFailure)
                return BadRequest();

            return Ok(_mapper.Map<ImageDto>(imageResult.Value));
        }
    }

    internal class ImagesControllerProfile : Profile
    {
        public ImagesControllerProfile()
        {
            CreateMap<Image, ImageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName.Value))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath.Value));
        }
    }
}
