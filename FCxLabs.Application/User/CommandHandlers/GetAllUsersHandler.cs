using FCxLabs.Application.Common.DTOs;
using FCxLabs.Application.Extensions;
using FcxLabs.Application.User.Queries;
using FCxLabs.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.CommandHandlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ObjectResult>
{
    private readonly IUserService _userService;

    public GetAllUsersHandler(IUserService userService)
	{
        _userService = userService;
    }
	
	public async Task<ObjectResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		var users = (await _userService.GetAllUsersAsync()).ToDTO();
		
		if(users.Count < 0)
		{
			return new ObjectResult(new Response { Status = "Success", Message = "There's no user." })
			{
				StatusCode = StatusCodes.Status204NoContent
			};
		}
		
		return new ObjectResult(new Response { Status = "Success", Content = users })
		{
			StatusCode = StatusCodes.Status200OK
		};
	}
}
