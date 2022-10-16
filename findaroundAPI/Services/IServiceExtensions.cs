using System;
using findaroundAPI.Entities;

namespace findaroundAPI.Services
{
	public static class IServiceExtensions
	{
		public static bool CheckIfUserLoggedIn(this IService service, DatabaseContext dbContext, IUserContextService contextService)
		{
			var user = dbContext.Users.FirstOrDefault(u => u.Id == contextService.GetUserId);

			if (user is null)
				return false;

			return user.LoggedIn;
		}
	}
}

