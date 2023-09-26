using FCxLabs.Application.Common.DTOs;
using FCxLabs.Application.Extensions;
using FcxLabs.Application.User.Queries;
using FCxLabs.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.CommandHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ObjectResult>
{
	private readonly IUserService _userService;

	public GetUserByIdQueryHandler(IUserService userService)
	{
		_userService = userService;
	}
	
	public async Task<ObjectResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = (await _userService.GetByIdAsync(request.Id)).ToDTO();
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be fetched." })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		return new ObjectResult(new Response { Status = "Success", Content = user })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
