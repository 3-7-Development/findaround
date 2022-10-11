using System;
using findaroundShared.Models.Dtos;

namespace findaroundAPI.Services
{
	public interface IUserService
	{
		void RegisterUser(RegisterUserDto dto);
		string LogInUser(LoginUserDto dto);
		void LogOutUser(int id);
	}
}

