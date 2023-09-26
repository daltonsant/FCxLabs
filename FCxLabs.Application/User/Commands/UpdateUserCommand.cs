using FCxLabs.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.Commands;

public class UpdateUserCommand : IRequest<ObjectResult>
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public string UserName { get; set; }
	public string CellPhone { get; set; }
	public string Email { get; set; }
	public Status Status { get; set; }
	public string Cpf { get; set; }
	public DateTime BirthDate { get; set; }
	public string MothersName { get; set; }
}
