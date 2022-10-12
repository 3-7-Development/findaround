using System;
using System.Security.Claims;
using findaroundAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace findaroundAPI.Authorization.Handlers
{
	public class PostOperationAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirements, PostEntity>
	{
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirements requirement, PostEntity post)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (post.AuthorId == userId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

