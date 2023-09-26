using FCxLabs.Core.Entities;

namespace FCxLabs.Core.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<IUserResult> LogicalDeleteAllAsync(IEnumerable<string> ids);
    public Task<IUserResult> LogicalDeleteAsync(User user);
    public Task<bool> AlreadyHasUserNameAsync(string username);
    public Task<bool> AlreadyHasCpfAsync(string cpf);
    public Task<bool> AlreadyHasEmailAsync(string email);
}