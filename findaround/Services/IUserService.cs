using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Services
{
	public interface IUserService : IWebService
	{
		Task<bool> RegisterUser(RegisterUserDto dto);
		Task<bool> LogInUser(LoginUserDto dto);
		Task<bool> LogOutUser();
		Task<User> GetUserData(int userId);
		Task<string> GetUserLogin(int userId);
	}
}

