using FCxLabs.Core.Contracts;
using FCxLabs.Core.Enums;

namespace FCxLabs.Application.Common.DTOs.User;

public class UserDto: IUser
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CellPhone { get; set; }
    public string UserName { get; set; }
    public Status Status { get; set; }
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MothersName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
