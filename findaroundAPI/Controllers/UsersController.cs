using System;
using findaroundShared.Models;
using findaroundAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using findaroundAPI.Services;
using findaroundShared.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace findaroundAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/findaround/users")]
	[Authorize]
	public class UsersController : ControllerBase
	{
		readonly DatabaseContext _dbContext;
		readonly IMapper _mapper;
		readonly IUserService _userService;

		public UsersController(DatabaseContext context, IMapper mapper, IUserService userService)
		{
            _dbContext = context;
			_mapper = mapper;
			_userService = userService;
		}

		[HttpGet("all")]
		public ActionResult<List<User>> GetAllUsers()
		{
			var users = _dbContext.Users.ToList();

			var usersList = new List<User>();

			if (users is null || users.Count == 0)
				return NotFound("No users in database");

            foreach (var user in users)
            {
                usersList.Add(_mapper.Map<User>(user));
            }

            return Ok(usersList);
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
		{
			var result = _userService.RegisterUser(dto);

			return result.GetResult();
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public ActionResult LogInUser([FromBody] LoginUserDto dto)
		{
			var result = _userService.LogInUser(dto);

			return result.GetResult();
		}

		[HttpPost("logout/{id}")]
		public ActionResult LogOutUser([FromRoute] int id)
		{
			var result = _userService.LogOutUser(id);

			return result.GetResult();
		}
	}
}

