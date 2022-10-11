using System;
using findaroundShared.Models.Dtos;

namespace findaroundAPI.Services
{
	public interface IUserService
	{
		void RegisterUser(RegisterUserDto dto);
		string LoginUser(LoginUserDto dto);
	}
}

