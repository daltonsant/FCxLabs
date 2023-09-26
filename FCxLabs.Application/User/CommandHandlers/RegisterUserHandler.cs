using FCxLabs.Application.Common.DTOs;
using FCxLabs.Application.Extensions;
using FcxLabs.Application.User.Commands;
using FCxLabs.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.CommandHandlers;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, ObjectResult>
{
    private readonly IUserService _userService;

	public RegisterUserHandler(IUserService userService)
	{
        _userService = userService;
	}

	public async Task<ObjectResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		if(!request.Cpf.IsCPFValid())
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User CPF is not valid." })
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity	
			};
		}
		
		if(!request.Password.IsPasswordValid())
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Password doesn't meet with one of the following criterias: Special Character, Upper Character." })
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity	
			};
		}
		
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
			MothersName = request.MothersName,
			Name = request.Name,
			CellPhone = request.CellPhone,
			SecurityStamp = Guid.NewGuid().ToString(),
			Status = FCxLabs.Core.Enums.Status.Active,
			UserName = request.UserName
		};
		
		var result = await _userService.CreateUserAsync(user, request.Password);
		
		if(!result)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be created." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		return new ObjectResult(new Response { Status = "Success", Message = "User created successfully!" })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
