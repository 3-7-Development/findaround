using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using findaroundAPI.Config;
using findaroundAPI.Entities;
using findaroundAPI.Exceptions;
using findaroundAPI.Utilities;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace findaroundAPI.Services
{
	public class UserService : IUserService
	{
        readonly DatabaseContext _dbContext;
        readonly AuthenticationSettings _authenticationSettings;

		public UserService(DatabaseContext dbContext, AuthenticationSettings authenticationSettings)
		{
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
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

        public string LogInUser(LoginUserDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == dto.Login);

            if (user is null)
                throw new LoginUserException("User not found");

            var arePasswordsEqual = PasswordsUtilities.ArePasswordsEqual(user, dto.Password);

            if (!arePasswordsEqual)
                throw new LoginUserException("Invalid password");

            var token = GenerateJwtToken(user);

            user.LoggedIn = true;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return token;
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public void LogOutUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new ArgumentException("Cannot log out");

            if (!user.LoggedIn)
                throw new ArgumentException("Something went wrong");

            user.LoggedIn = false;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
    }
}

