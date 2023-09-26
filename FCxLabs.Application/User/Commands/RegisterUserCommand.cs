using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.Commands;

public class RegisterUserCommand : IRequest<ObjectResult>
{
	public string Name { get; set; }
	public string Password { get; set; }
	public string UserName { get; set; }
	public string CellPhone { get; set; }
	public string Email { get; set; }
	public string Cpf { get; set; }
	public DateTime BirthDate { get; set; }
	public string MothersName { get; set; }	
	public string Scheme { get; set; }
	public IUrlHelper Url { get; set; }
}
