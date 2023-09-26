using FCxLabs.Core.Contracts;
using FCxLabs.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace FCxLabs.Core.Entities;

public class User : IdentityUser, IUser
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string Name { get; set; }
    public string CellPhone { get; set; }
    public Status Status { get; set; }
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MothersName { get; set; }
}