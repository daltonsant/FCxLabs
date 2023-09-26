using FCxLabs.Application.Common.DTOs.Auth.SignUp;
using FCxLabs.Application.Common.DTOs.User;
using FcxLabs.Application.User.Commands;
using FCxLabs.Core.Contracts;
using FCxLabs.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FCxLabs.Application.Extensions;

public static class DtoExtensions
{
    public static RegisterUserCommand ToRegisterUserCommand(this RegisterUserDto registerUser, string scheme, IUrlHelper url)
    {
        return new RegisterUserCommand
        {
            Name = registerUser.Name,
            BirthDate = registerUser.BirthDate,
            Cpf = registerUser.Cpf,
            Email = registerUser.Email,
            UserName = registerUser.UserName,
            CellPhone = registerUser.CellPhone,
            MothersName = registerUser.MothersName,
            Password = registerUser.Password,
            Scheme = scheme,
            Url = url
        };
    }
	
    public static CreateUserWithRoleCommand ToCreateUserWithRoleCommand(this RegisterUserDto registerUser, string role)
    {
        return new CreateUserWithRoleCommand
        {
            Name = registerUser.Name,
            BirthDate = registerUser.BirthDate,
            Cpf = registerUser.Cpf,
            Email = registerUser.Email,
            UserName = registerUser.UserName,
            MobilePhone = registerUser.CellPhone,
            MotherName = registerUser.MothersName,
            Password = registerUser.Password,
            Role = role
        };
    }
	
    public static UpdateUserCommand ToUpdateUserCommand(this UpdateUserDto user, string id)
    {
        return new UpdateUserCommand
        {
            Id = id,
            Name = user.Name,
            BirthDate = user.BirthDate,
            Cpf = user.Cpf,
            Email = user.Email,
            UserName = user.UserName,
            CellPhone = user.MobilePhone,
            MothersName = user.MotherName,
            Password = user.Password,
            Status = user.Status,
        };
    }
	
    public static IUser ToDTO(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            BirthDate = user.BirthDate,
            Cpf = user.Cpf,
            Email = user.Email,
            CreatedOn = user.CreatedOn,
            CellPhone = user.CellPhone,
            ModifiedOn = user.ModifiedOn,
            MothersName = user.MothersName,
            Name = user.Name,
            Status = user.Status,
            UserName = user.UserName
        };
    }
	
    public static IReadOnlyList<IUser>ToDTO(this IReadOnlyList<User> users)
    {
        var usersDTO = new List<IUser>();
        foreach(var user in users)
        {
            usersDTO.Add(user.ToDTO());
        }
        return usersDTO;
    }
}