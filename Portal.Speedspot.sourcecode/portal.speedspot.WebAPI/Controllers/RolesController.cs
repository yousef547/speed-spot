using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.WebAPI.Infrastracture;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace portal.speedspot.WebAPI.Controllers
{
    public class RolesController : BaseApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public RolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IMapper mapper) : base(mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Permissions.Roles.View)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleManager.GetAllAsync();
            var roleVMs = roles.Where(r => r.Name != "SuperAdmin").Select(r => _mapper.Map<RoleViewModel>(r)).ToList();
            return Ok(roleVMs);
        }

        [Authorize(Permissions.Roles.ViewArchived)]
        [HttpGet("Archive")]
        public async Task<IActionResult> GetAllArchived()
        {
            var roles = await _roleManager.GetAllAsync(true);
            var roleVMs = roles.Where(r => r.Name != "SuperAdmin").Select(r => _mapper.Map<RoleViewModel>(r)).ToList();
            return Ok(roleVMs);
        }

        [HttpGet("{id}")]
        [Authorize(Permissions.Roles.Details)]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var viewModel = _mapper.Map<RoleViewModel>(role);
                return Ok(viewModel);
            }
            return NotFound();
        }

        [HttpGet("{id}/Permissions")]
        [Authorize(Permissions.Roles.ManagePermissions)]
        public async Task<IActionResult> GetRolePermissions(string id)
        {
            PermissionViewModel permissionVM = new PermissionViewModel();
            permissionVM.RoleId = id;
            permissionVM.RolePermissionsVM = new List<RolePermissionsViewModel>();
            Type type = typeof(Permissions);
            var modules = typeof(Permissions).GetNestedTypes().ToList();
            foreach (var module in modules)
            {
                var permissions = module.GetFields(BindingFlags.Public | BindingFlags.Static).Select(p => (string)p.GetRawConstantValue()).ToList();
                permissionVM.RolePermissionsVM.Add(new RolePermissionsViewModel
                {
                    Module = module.Name,
                    PermissionVMs = permissions.Select(p => new RoleClaimsViewModel
                    {
                        Type = "Permission",
                        Value = p
                    }).ToList()
                });
            }

            var role = await _roleManager.FindByIdAsync(id);
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = permissionVM.RolePermissionsVM.SelectMany(rp => rp.PermissionVMs).Select(p => p.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var rolePermission in permissionVM.RolePermissionsVM)
            {
                foreach (var permission in rolePermission.PermissionVMs)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;
                    }
                }
            }
            return Ok(permissionVM);
        }

        [HttpPut("{id}/Permissions")]
        [Authorize(Permissions.Roles.ManagePermissions)]
        public async Task<IActionResult> UpdatePermissions(PermissionViewModel permissionViewModel)
        {
            var role = await _roleManager.FindByIdAsync(permissionViewModel.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            var selectedClaims = permissionViewModel.RolePermissionsVM.SelectMany(rp => rp.PermissionVMs).Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.Value));
            }

            return Ok(new
            {
                Message = "Updated successfully"
            });
        }

        [HttpPost("")]
        [Authorize(Permissions.Roles.Create)]
        public async Task<IActionResult> Post(RoleViewModel model)
        {
            var role = _mapper.Map<ApplicationRole>(model);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Message = Messages.CreateSuccessfull
                });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut("")]
        [Authorize(Permissions.Roles.Edit)]
        public async Task<IActionResult> Put(RoleViewModel model)
        {
            var role = _mapper.Map<ApplicationRole>(model);
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Message = Messages.UpdateSuccessfull
                });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut("Archive/{id}")]
        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Archive(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleUsers = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in roleUsers)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            role.IsArchived = true;
            await _roleManager.ArchiveAsync(role);

            return Ok(new
            {
                Message = Messages.ArchiveSuccessfull
            });
        }

        [HttpPut("Unarchive/{id}")]
        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Unarchive(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleUsers = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in roleUsers)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            role.IsArchived = false;
            await _roleManager.UnarchiveAsync(role);

            return Ok(new
            {
                Message = Messages.UnarchiveSuccessfull
            });
        }
    }
}
