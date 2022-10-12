using System;
using System.Security.Claims;
using findaroundAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace findaroundAPI.Authorization.Handlers
{
	public class UserOperationAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirements, UserEntity>
	{
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirements requirement, UserEntity user)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (user.Id == userId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

