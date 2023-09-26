using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.Commands;

public class CreateUserWithRoleCommand : IRequest<ObjectResult>
{
	public string Name { get; set; }
	public string Password { get; set; }
	public string UserName { get; set; }
	public string MobilePhone { get; set; }
	public string Email { get; set; }
	public string Cpf { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
	public string Role { get; set; }
}
