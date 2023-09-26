using FCxLabs.Application.Common.DTOs;
using FcxLabs.Application.User.Commands;
using FCxLabs.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.CommandHandlers;

public class CreateUserWithRoleHandler : IRequestHandler<CreateUserWithRoleCommand, ObjectResult>
{
	private readonly IUserService _userService;

	public CreateUserWithRoleHandler(IUserService userService)
	{
		_userService = userService;
	}
	
	public async Task<ObjectResult> Handle(CreateUserWithRoleCommand request, CancellationToken cancellationToken)
	{
		var userExists = await _userService.GetUserByEmailAsync(request.Email);
		
		if(userExists is not null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User already exists!" })
			{
				StatusCode = StatusCodes.Status403Forbidden
			};
		}
		
		FCxLabs.Core.Entities.User user = new()
		{
			Email = request.Email,
			BirthDate = request.BirthDate,
			Cpf = request.Cpf,
			UserName = request.UserName,
			MothersName = request.MotherName,
			Name = request.Name,
			CellPhone = request.MobilePhone,
			SecurityStamp = Guid.NewGuid().ToString(),
			Status = FCxLabs.Core.Enums.Status.Active
		};
		
		var roleExists = await _userService.RoleExistsAsync(request.Role);
		
		if(!roleExists)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Failed To Create." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};
		}
		
		var result = await _userService.CreateUserAsync(user, request.Password);
			
		if(!result)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Failed To Create." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		await _userService.AddToRoleAsync(user, request.Role);
		
		var token = await _userService.GenerateEmailConfirmationTokenAsync(user);
		
		await _userService.ConfirmEmailAsync(user, token);
		
		return new ObjectResult(new Response { Status = "Success", Message = "User Created Successfully!" })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
