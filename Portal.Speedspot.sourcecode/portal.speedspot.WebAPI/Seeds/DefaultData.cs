using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace portal.speedspot.WebAPI.Seeds
{
    public static class DefaultData
    {
        public static async Task SeedDefaultData(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            var defaultRole = new ApplicationRole
            {
                Name = "SuperAdmin"
            };
            var roleExists = await roleManager.RoleExistsAsync(defaultRole.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(defaultRole);
            }

            var defaultUser = new ApplicationUser
            {
                UserName = "ahmed.samir@atcode.net",
                Email = "ahmed.samir@atcode.net",
                PhoneNumber = "00201128169883",
                EmailConfirmed = true,
                FirstName = "Ahmed",
                LastName = "Samir",
                IsArchived = false
            };
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Qazwsx123!@#");
            }

            var userInRole = await userManager.IsInRoleAsync(user ?? defaultUser, defaultRole.Name);
            if (!userInRole)
            {
                await userManager.AddToRoleAsync(user ?? defaultUser, defaultRole.Name);
            }
        }
    }
}
