using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Main.App.User;
using PcbManager.Main.WebApi.Customization;
using PcbManager.Main.WebApi.Dtos;
using PcbManager.Main.WebApi.ErrorHandler;

namespace PcbManager.Main.WebApi.Controllers;

[ApiController]
[StandardRoute]
public class UsersController : ControllerBase
{
    private readonly IUserAppService _userAppService;

    public UsersController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll() =>
        await _userAppService.GetAllAsync()
            .Match(users => Ok(users.Select(UserDto.From)), ErrorMapper.Map);

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id) =>
        await _userAppService.GetByIdAsync(id)
            .Match(user => Ok(UserDto.From(user)), ErrorMapper.Map);

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserRequest createUserRequest) =>
        await _userAppService.CreateAsync(createUserRequest)
            .Match(user => Ok(UserDto.From(user)), ErrorMapper.Map);

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<UserDto>> Delete(Guid id) =>
        await _userAppService.DeleteAsync(id)
            .Match(user => Ok(UserDto.From(user)), ErrorMapper.Map);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, UpdateUserRequest updateUserRequest) =>
        await _userAppService.UpdateAsync(id, updateUserRequest)
            .Match(user => Ok(UserDto.From(user)), ErrorMapper.Map);
}