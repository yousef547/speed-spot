using portal.speedspot.BizLayer.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using portal.speedspot.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace portal.speedspot.WebUI.Helpers
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        ApplicationUserManager _userManager;
        ApplicationRoleManager _roleManager;

        public PermissionAuthorizationHandler(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            if (context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Super Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var permissions = context.User.Claims
                .Where(x => x.Type == CustomClaimTypes.Permission && 
                x.Value == requirement.Permission && 
                x.Issuer == "LOCAL AUTHORITY")
                .Select(x => x.Value);

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            //var user = await _userManager.GetUserAsync(context.User);
            //if (user == null)
            //{
            //    return;
            //}

            //var userRoleNames = await _userManager.GetRolesAsync(user);

            //var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));
            //foreach (var role in userRoles)
            //{
            //    var roleClaims = await _roleManager.GetClaimsAsync(role);
            //    var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
            //                                            x.Value == requirement.Permission &&
            //                                            x.Issuer == "LOCAL AUTHORITY")
            //                                .Select(x => x.Value);

            //    if (permissions.Any())
            //    {
            //        context.Succeed(requirement);
            //        return;
            //    }
            //}
        }
    }
}
