using FCxLabs.Core.Enums;

namespace FCxLabs.Core.Contracts;

public interface IUser : IEntity
{
    public string Name { get; set; }
    public string CellPhone { get; set; }
    public Status Status { get; set; }
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MothersName { get; set; }
}