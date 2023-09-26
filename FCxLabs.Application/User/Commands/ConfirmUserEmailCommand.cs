using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.Commands;

public class ConfirmUserEmailCommand: IRequest<ObjectResult>
{
	public string Token { get; set; }
	public string Email { get; set; }
}
