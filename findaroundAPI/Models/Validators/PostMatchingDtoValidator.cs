using System;
using findaroundAPI.Entities;
using findaroundShared.Models.Dtos;
using FluentValidation;

namespace findaroundAPI.Models.Validators
{
	public class PostMatchingDtoValidator : AbstractValidator<PostMatchingDto>
    {
		public PostMatchingDtoValidator(DatabaseContext dbContext)
		{
			RuleFor(p => p.Category).NotEmpty();

			RuleFor(p => p.Location).NotEmpty()
				.Custom((value, context) =>
				{
					if (value.Latitude < 0.00 || value.Longitude < 0.00)
						context.AddFailure("Location", "Invalid location");
				});
		}
	}
}

