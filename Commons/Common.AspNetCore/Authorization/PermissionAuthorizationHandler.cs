using Microsoft.AspNetCore.Authorization;

namespace Common.AspNetCore.Authorization;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // 从 JWT 中读取 permissions claim
        var permissions = context.User
            .FindAll("permission")
            .Select(c => c.Value);

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}