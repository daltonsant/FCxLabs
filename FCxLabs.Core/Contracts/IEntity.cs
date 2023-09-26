namespace FCxLabs.Core.Contracts;

public interface IEntity
{
    public string Id { get; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}