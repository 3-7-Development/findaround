using System;
using findaroundShared.Models.Dtos;
using LanguageExt.Common;

namespace findaroundAPI.Services
{
	public interface IUserService
	{
		Result<int> RegisterUser(RegisterUserDto dto);
		Result<string> LogInUser(LoginUserDto dto);
		Result<string> LogOutUser(int id);
	}
}

