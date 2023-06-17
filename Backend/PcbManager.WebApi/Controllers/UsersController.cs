using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PcbManager.App;
using PcbManager.Domain.UserNS;
using PcbManager.Domain.UserNS.ValueObjects;
using PcbManager.WebApi.Customization;
using PcbManager.WebApi.Dtos;

namespace PcbManager.WebApi.Controllers
{
    [ApiController]
    [StandardRoute]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;

        public UsersController(IUserAppService userAppService, IMapper mapper)
        {
            _userAppService = userAppService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserRequest createUserRequest)
        {
            var userResult = await _userAppService.CreateAsync(createUserRequest);

            if(userResult.IsFailure)
                return BadRequest();

            return Ok(_mapper.Map<UserDto>(userResult.Value));
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var maybeUsers = await _userAppService.GetAllAsync();

            if (maybeUsers.HasNoValue)
                return NotFound();

            return Ok(_mapper.Map<List<UserDto>>(maybeUsers.Value));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var maybeUser = await _userAppService.GetByIdAsync(UserId.Create(id).Value);

            if (maybeUser.HasNoValue)
                return NotFound();

            return Ok(_mapper.Map<UserDto>(maybeUser.Value));
        }
    }

    internal class UsersControllerProfile : Profile
    {
        public UsersControllerProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
        }
    }
}