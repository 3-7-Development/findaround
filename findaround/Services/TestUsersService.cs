using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Services
{
	public class TestUsersService : IUserService
	{
		public TestUsersService()
		{
		}

        public Task<User> GetUserData(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserLogin(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogInUser(LoginUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogOutUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

