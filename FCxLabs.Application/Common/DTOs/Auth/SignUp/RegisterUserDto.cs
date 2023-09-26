using System.ComponentModel.DataAnnotations;

namespace FCxLabs.Application.Common.DTOs.Auth.SignUp;

public class RegisterUserDto
{
	[Required(ErrorMessage = "Name is required.")]
	public string Name { get; set; }
		
	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; set; }
		
	[Required(ErrorMessage = "Login/UserName is required.")]
	public string UserName { get; set; }
		
	[Required(ErrorMessage = "CellPhone is required.")]
	public string CellPhone { get; set; }
		
	[EmailAddress]
	[Required(ErrorMessage = "Email is required.")]
	public string Email { get; set; }
		
	[Required(ErrorMessage = "CPF is required.")]
	public string Cpf { get; set; }
		
	[Required(ErrorMessage = "BirthDate is required.")]
	public DateTime BirthDate { get; set; }
		
	[Required(ErrorMessage = "MothersName is required.")]
	public string MothersName { get; set; }    
}
	
