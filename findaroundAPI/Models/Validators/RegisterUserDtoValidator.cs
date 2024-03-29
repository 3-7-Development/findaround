﻿using System;
using findaroundAPI.Entities;
using findaroundShared.Models.Dtos;
using FluentValidation;

namespace findaroundAPI.Models.Validators
{
	public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
	{
		public RegisterUserDtoValidator(DatabaseContext dbContext)
		{
			RuleFor(x => x.Login).NotEmpty().MinimumLength(8).Custom((value, context) =>
			{
				var usernameInUse = dbContext.Users.Any(u => u.Login == value);

				if (usernameInUse)
					context.AddFailure("Login", "Login in use");
			});

			RuleFor(x => x.Password).NotEmpty().MinimumLength(6);

			RuleFor(x => x.ConfirmedPassword).NotEmpty().Equal(x => x.Password);
        }
	}
}

