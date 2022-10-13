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
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace findaroundAPI.Services
{
	public class UserService : IUserService
	{
        readonly DatabaseContext _dbContext;
        readonly AuthenticationSettings _authenticationSettings;
        readonly IAuthorizationService _authorizationService;
        readonly IUserContextService _userContextService;

        public UserService(DatabaseContext dbContext, AuthenticationSettings authenticationSettings,
            IAuthorizationService authorizationService, IUserContextService userContextService)
		{
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public Result<int> RegisterUser(RegisterUserDto dto)
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

            return new Result<int>(1);
        }

        public Result<string> LogInUser(LoginUserDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == dto.Login);

            if (user is null)
            {
                var exception = new LoginUserException("User not found");
                return new Result<string>(exception);
            }

            var arePasswordsEqual = PasswordsUtilities.ArePasswordsEqual(user, dto.Password);

            if (!arePasswordsEqual)
            {
                var exception = new LoginUserException("Invalid password");
                return new Result<string>(exception);
            }

            var token = GenerateJwtToken(user);

            user.LoggedIn = true;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return new Result<string>(token);
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

        public Result<string> LogOutUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
            {
                var exception = new ArgumentException("Cannot log out");
                return new Result<string>(exception);
            }

            if (!user.LoggedIn)
            {
                var exception = new ArgumentException("Something went wrong");
                return new Result<string>(exception);
            }

            user.LoggedIn = false;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return new Result<string>("Success");
        }
    }
}

