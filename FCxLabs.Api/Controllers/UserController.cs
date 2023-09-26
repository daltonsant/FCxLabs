using FCxLabs.Application.Common.DTOs.Auth.SignUp;
using FCxLabs.Application.Common.DTOs.User;
using FCxLabs.Application.Extensions;
using FcxLabs.Application.User.Commands;
using FcxLabs.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCxLabs.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
	
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
	
    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto userForm, string role)
    {
        return await _mediator.Send(userForm.ToCreateUserWithRoleCommand(role));
    }
	
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        return await _mediator.Send(new GetUserByIdQuery{ Id = id});
    }
	
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }
	
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto userForm, [FromRoute]string id)
    {
        return await _mediator.Send(userForm.ToUpdateUserCommand(id));
    }
	
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        return await _mediator.Send(new DeleteUserCommand { Id = id});
    }
}
