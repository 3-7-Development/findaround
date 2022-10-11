using System;
using findaroundAPI.Entities;
using findaroundShared.Models.Dtos;
using FluentValidation;

namespace findaroundAPI.Models.Validators
{
	public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
	{
		public LoginUserDtoValidator(DatabaseContext dbContext)
		{
			RuleFor(x => x.Login).NotEmpty().MinimumLength(8).Custom((value, context) =>
			{
				var userExists = dbContext.Users.Any(u => u.Login == value);

				if (!userExists)
					context.AddFailure("Login", "User not found");
			});

			RuleFor(x => x.Password).NotEmpty().MinimumLength(6);

			RuleFor(x => x.ConfirmedPassword).NotEmpty().Equal(x => x.Password);
		}
	}
}

