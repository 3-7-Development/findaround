using System;
using Microsoft.AspNetCore.Authorization;

namespace findaroundAPI.Authorization
{
	public class ResourceOperationRequirements : IAuthorizationRequirement
	{
		public ResourceOperationRequirements()
		{
		}
	}
}

