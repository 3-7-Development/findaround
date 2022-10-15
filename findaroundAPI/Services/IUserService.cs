using System;
using findaroundAPI.Entities;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using LanguageExt.Common;

namespace findaroundAPI.Services
{
	public interface IUserService
	{
		Result<int> RegisterUser(RegisterUserDto dto);
		Result<string> LogInUser(LoginUserDto dto);
		Result<string> LogOutUser(int id);
		Result<User> GetUserBasicInfo(int userId);
		Result<User> GetInfoAboutYourself();
		Result<string> GetUserLogin(int userId);
    }
}

