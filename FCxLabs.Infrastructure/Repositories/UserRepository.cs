using FCxLabs.Core.Contracts;
using FCxLabs.Core.Contracts.Repositories;
using FCxLabs.Core.Entities;
using FCxLabs.Infrastructure.DbContext;
using FCxLabs.Infrastructure.IdentityModels;
using Microsoft.EntityFrameworkCore;

namespace FCxLabs.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(UserDbContext db) : base(db)
    {
    }

    public async Task<IUserResult> LogicalDeleteAllAsync(IEnumerable<string> ids)
    {
        var userResults = new List<IUserResult>();
        var userResult = new UserResult();
		
        foreach(var user in _db.Users)
        {
            if(user is not null)
            {
                var result = await LogicalDeleteAsync(user);
                userResults.Add(result);
            }
        }
		
        foreach(var res in userResults)
        {
            if(res.Errors.Count > 0)
                userResult.Errors.AddRange(res.Errors);
        }
        return userResult;
    }

    public async Task<IUserResult> LogicalDeleteAsync(User user)
    {
        var userResult = new UserResult();
        try
        {
            user.Status = Core.Enums.Status.Inactive;
            _db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return userResult;
        }
        catch(DbUpdateException ex)
        {
            var message = $"LogicalDelete: A db error ocurred; Message: {ex.Message}; StackTrace: {ex.StackTrace}";
            userResult.Errors.Add(message);
            return userResult;
        }
    }

    public async Task<bool> AlreadyHasUserNameAsync(string username)
    {
        var result = await _db.Set<User>().AnyAsync(u => u.UserName == username);
        return result;;
    }

    public async Task<bool> AlreadyHasCpfAsync(string cpf)
    {
        var result = await _db.Set<User>().AnyAsync(u => u.Cpf == cpf);
        return result;
    }

    public async Task<bool> AlreadyHasEmailAsync(string email)
    {
        var result = await _db.Set<User>().AnyAsync(u => u.Email == email);
        return result;
    }
}