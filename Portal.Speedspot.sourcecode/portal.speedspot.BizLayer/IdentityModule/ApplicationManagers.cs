using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.BizLayer.IdentityModule
{
    public sealed class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        { }

        public async Task<IList<ApplicationUser>> GetAllUsersAsync(bool isArchived = false)
        {
            return await Users.Where(u => u.IsArchived == isArchived).ToListAsync();
        }
        public async Task<IList<ApplicationUser>> GetAllUsersExceptAsync(string userId)
        {
            return await Users.Where(u => !u.IsArchived && u.Id != userId).ToListAsync();
        }
        public async Task<IdentityResult> UpdateProfilePicture(ApplicationUser user, string picturePath)
        {
            user.ProfilePicture = picturePath;
            return await UpdateAsync(user);
        }

        public async Task<IdentityResult> ArchiveAsync(ApplicationUser user)
        {
            user.IsArchived = true;
            return await UpdateAsync(user);
        }

        public async Task<IdentityResult> UnarchiveAsync(ApplicationUser user)
        {
            user.IsArchived = false;
            return await UpdateAsync(user);
        }

        public async Task<bool> IsSuperAdmin(ApplicationUser user)
        {
            return await IsInRoleAsync(user, "Super Admin");
        }

        public async Task<IList<ApplicationUser>> GetSalesAgentsAsync()
        {
            return await GetUsersInRoleAsync("Sales Agent");
        }


        public async Task<IList<ApplicationUser>> GetManagersAsync()
        {
            return await GetUsersInRoleAsync("Manager");

        }

        public async Task<IList<ApplicationUser>> GetMyEmployeesAsync(string userId)
        {
            return await Users
                .Include(x => x.DirectManager.DirectManager)
                .Include(x => x.Supervisors)
                .Where(u => u.DirectManagerId == userId ||
                u.DirectManager.DirectManagerId == userId ||
                u.DirectManager.DirectManager.DirectManagerId == userId ||
                u.Supervisors.Any(s => s.SupervisorId == userId)).ToListAsync();
        }

        public async Task<IList<ApplicationUser>> GetMyManagersAsync(string userId)
        {
            return await Users
                .Include(x => x.Employees).ThenInclude(y => y.Employees).ThenInclude(z => z.Employees)
                .Where(u => u.Employees.Any(x => x.Id == userId) ||
                u.Employees.SelectMany(ue => ue.Employees).Any(x => x.Id == userId) ||
                u.Employees.SelectMany(ue => ue.Employees).SelectMany(uee => uee.Employees).Any(x => x.Id == userId))
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await Users
                .Include(x => x.DirectManager)
                .Include(x => x.EmployeeType)
                .Include(x => x.Department)
                .Include(x => x.UserRoles).ThenInclude(y => y.Role)
                .Include(x => x.UserDepartments).ThenInclude(y => y.Department)
                .Include(x => x.Supervisors)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await Users
                .Include(x => x.DirectManager)
                .Include(x => x.EmployeeType)
                .Include(x => x.Department)
                .Include(x => x.UserDepartments).ThenInclude(y => y.Department)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }

    public sealed class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> roleStore, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger)
            : base(roleStore, roleValidators, keyNormalizer, errors, logger)
        { }

        public async Task<IList<ApplicationRole>> GetAllAsync(bool isArchived = false)
        {
            return await Roles.Where(r => r.IsArchived == isArchived).ToListAsync();
        }

        public Task<IdentityResult> ArchiveAsync(ApplicationRole role)
        {
            role.IsArchived = true;
            return UpdateAsync(role);
        }

        public Task<IdentityResult> UnarchiveAsync(ApplicationRole role)
        {
            role.IsArchived = false;
            return UpdateAsync(role);
        }
    }

    //public sealed class ApplicationSignInManager : SignInManager<ApplicationUser>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor = null, ILogger<SignInManager<ApplicationUser>> logger = null, IAuthenticationSchemeProvider schemes = null)
    //       : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
    //    {
    //    }
    //}

    //public sealed class ApplicationClaimsPrincipleFactory : IUserClaimsPrincipalFactory<ApplicationUser>
    //{
    //    public Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    //    {
    //        return Task.Factory.StartNew(() =>
    //        {
    //            var identity = new ClaimsIdentity();
    //            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
    //            var principle = new ClaimsPrincipal(identity);

    //            return principle;
    //        });
    //    }
    //}
}
