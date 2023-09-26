using System.Linq.Expressions;
using FCxLabs.Core.Contracts.Repositories;
using FCxLabs.Core.Contracts.Services;
using FCxLabs.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FCxLabs.Infrastructure.Services;

public class UserService : BaseService<User>, IUserService
{
	private readonly IUserRepository _db;
	private readonly UserManager<User> _userManager;
	private readonly ILogger<UserService> _userServiceLogger;
	private readonly RoleManager<IdentityRole> _roleManager;

	public UserService(IUserRepository db, UserManager<User> userManager, ILogger<UserService> userServiceLogger, RoleManager<IdentityRole> roleManager) : base(db)
	{
		_db = db;
		_userManager = userManager;
		_userServiceLogger = userServiceLogger;
		_roleManager = roleManager;
	}

	public Task<IReadOnlyList<User>> GetAllUsersAsync()
	{
		return _db.GetAllAsync();
	}

	public Task<IReadOnlyList<User>> GetUsersAsync(Expression<Func<User, bool>> predicate = null)
	{
		return _db.GetAsync(predicate);
	}

	public async Task<User> GetUserByIdAsync(string id)
	{
		var user = await _db.GetByIdAsync(id);
		return user;
	}

	public async Task<bool> DeleteAllSelectedByIdAsync(IEnumerable<string> ids)
	{
		var result = await _db.LogicalDeleteAllAsync(ids);
		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
			return result.Succeeded;
		}
		
		foreach(var errorMessage in result.Errors)
		{
			var index = 1;
			_userServiceLogger.LogError("UpdateUserAsync: User updated failed;Message {index}: {errorMessage}", index, errorMessage);
		}
		return false;
	}

	public async Task<bool> DeleteUserAsync(User user)
	{
		var result = await _db.LogicalDeleteAsync(user);
		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
			return result.Succeeded;
		}
		
		foreach(var errorMessage in result.Errors)
		{
			var index = 1;
			_userServiceLogger.LogError("UpdateUserAsync: User updated failed;Message {index}: {errorMessage}", index, errorMessage);
		}
		return false;
	}

	public async Task<bool> UpdateUserAsync(User user)
	{
		var result = await _userManager.UpdateAsync(user);

		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
			return result.Succeeded;
		}
		
		foreach(var error in result.Errors)
		{
			var index = 1;
			var message = error.Description;
			var code = error.Code;
			_userServiceLogger.LogError("UpdateUserAsync: User updated failed;Code: {code};Message {index}: {message}", index, message, code);
		}
		return false;
	}

	public async Task<bool> CreateUserAsync(User user, string password)
	{	
		var result = await _userManager.CreateAsync(user);

		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("CreateUserAsync: User created sucessfully; ResultValue: {result}", result);			
			return result.Succeeded;	
		}
		
		foreach(var error in result.Errors)
		{
			var index = 1;
			var message = error.Description;
			var code = error.Code;
			
			_userServiceLogger.LogError("CreateUserAsync: User created failed;Code: {code};Message {index}: {message}", index, message, code);
		}
		
		return false;
	}

	public async Task<User> GetUserByEmailAsync(string email)
	{
		var result = await _db.GetAsync(e => e.Email == email);
		if(result.Count > 0)
			return result[0];
		return null;
	}

	public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
	{	
		var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		return token;
	}

	public async Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
	{
		var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
		return token;
	}

	public async Task<bool> AlreadyHasUserName(string username)
	{
		return await _db.AlreadyHasUserNameAsync(username);
	}

	public async Task<bool> AlreadyHasCpf(string cpf)
	{
		return await _db.AlreadyHasCpfAsync(cpf);
	}

	public async Task<bool> AlreadyHasEmail(string email)
	{
		return await _db.AlreadyHasEmailAsync(email);
	}

	public async Task<bool> ConfirmEmailAsync(User user, string token)
	{
		var result = await _userManager.ConfirmEmailAsync(user, token);
		return result.Succeeded;
	}

	public async Task<bool> AddToRoleAsync(User user, string role)
	{
		var result = await _userManager.AddToRoleAsync(user, role);
		return result.Succeeded;
	}

	public async Task<bool> RoleExistsAsync(string role)
	{
		var result = await _roleManager.RoleExistsAsync(role);
		return result;
	}
	
	public async Task<bool> ChangeEmailAsync(User user, string newEmail, string token)
	{
		var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
		return result.Succeeded;
	}

	public async Task<string> GeneratePasswordResetTokenAsync(User user)
	{
		return await _userManager.GeneratePasswordResetTokenAsync(user);
	}

	public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
	{
		var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
		return result.Succeeded;
	}
}
