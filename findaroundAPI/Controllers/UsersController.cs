using System;
using findaroundShared.Models;
using findaroundAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace findaroundAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/findaround/users")]
	public class UsersController : ControllerBase
	{
		readonly DatabaseContext _dbContext;
		readonly IMapper _mapper;

		public UsersController(DatabaseContext context, IMapper mapper)
		{
            _dbContext = context;
			_mapper = mapper;
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
	}
}

