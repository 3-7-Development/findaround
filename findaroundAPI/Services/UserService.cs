using System;
using findaroundAPI.Entities;
using findaroundAPI.Exceptions;
using findaroundAPI.Utilities;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaroundAPI.Services
{
	public class UserService : IUserService
	{
        readonly DatabaseContext _dbContext;

		public UserService(DatabaseContext dbContext)
		{
            _dbContext = dbContext;
		}

        public void RegisterUser(RegisterUserDto dto)
        {
            var userPasswordData = PasswordsUtilities.HashPassword(dto.Password);

            var user = new UserEntity()
            {
                Login = dto.Login,
                PasswordHash = userPasswordData.PasswordHash,
                Salt = userPasswordData.Salt,
                ProfileImage = "defaultProfileImage.png",
                Posts = new List<PostEntity>(),
                Comments = new List<CommentEntity>(),
                LoggedIn = false
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public string LoginUser(LoginUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

