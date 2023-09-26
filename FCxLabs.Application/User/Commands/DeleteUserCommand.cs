using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.Commands;

public class DeleteUserCommand : IRequest<ObjectResult>
{
	public string Id { get; set; }
}
