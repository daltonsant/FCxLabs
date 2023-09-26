using FCxLabs.Application.Common.DTOs.Auth.SignUp;
using FCxLabs.Application.Extensions;
using FcxLabs.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCxLabs.Api.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> Signup([FromBody] RegisterUserDto userForm)
    {
        return await _mediator.Send(userForm.ToRegisterUserCommand(Request.Scheme, Url));
    }
    
}
