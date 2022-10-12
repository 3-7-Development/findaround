using System;
using System.Security.Claims;
using findaroundAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace findaroundAPI.Authorization.Handlers
{
	public class CommentOperationAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirements, CommentEntity>
	{
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirements requirement, CommentEntity comment)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (comment.AuthorId == userId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

