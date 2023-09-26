using System.Linq.Expressions;
using FCxLabs.Core.Entities;

namespace FCxLabs.Core.Contracts.Services;

public interface IUserService: IBaseService<User>
{
    public Task<bool> DeleteAllSelectedByIdAsync(IEnumerable<string> ids);
    public Task<bool> DeleteUserAsync(User user);
    public Task<bool> UpdateUserAsync(User user);
    public Task<bool> CreateUserAsync(User user, string password);
    public Task<IReadOnlyList<User>> GetAllUsersAsync();
    public Task<IReadOnlyList<User>> GetUsersAsync(Expression<Func<User, bool>> predicate = null);
    public Task<User> GetUserByIdAsync(string id);
    public Task<User> GetUserByEmailAsync(string email);
    public Task<string> GenerateEmailConfirmationTokenAsync(User user);
    public Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
    public Task<string> GeneratePasswordResetTokenAsync(User user);
    public Task<bool> AlreadyHasUserName(string username);
    public Task<bool> AlreadyHasCpf(string cpf);
    public Task<bool> AlreadyHasEmail(string email);
    public Task<bool> ConfirmEmailAsync(User user, string token);
    public Task<bool> AddToRoleAsync(User user, string role);
    public Task<bool> RoleExistsAsync(string role);
    public Task<bool> ChangeEmailAsync(User user, string newEmail, string token);
    public Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
}
