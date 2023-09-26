using FCxLabs.Core.Enums;

namespace FCxLabs.Application.Common.DTOs.User;

public class UpdateUserDto
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string MobilePhone { get; set; }
    public string Password { get; set; }
    public Status Status { get; set; }
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MotherName { get; set; }
    public string Email { get; set; }
}