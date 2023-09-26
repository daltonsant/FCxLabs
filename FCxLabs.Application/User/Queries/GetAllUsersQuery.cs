using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabs.Application.User.Queries;

public class GetAllUsersQuery : IRequest<ObjectResult>
{
}
