using portal.speedspot.BizLayer.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using portal.speedspot.WebUI.Helpers;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using AutoMapper;

namespace portal.speedspot.WebUI.Controllers
{
    public class RolesController : BaseController
    {
        private ApplicationRoleManager _roleManager;

        public RolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IMapper mapper)
            : base(mapper, userManager)
        {
            _roleManager = roleManager;
        }

        [Authorize(Permissions.Roles.View)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Where(r => r.Name != "Super Admin").Select(r => _mapper.Map<RoleViewModel>(r));
            return PartialView("_ShowRolesPartial",roles.ToList());
        }

        [Authorize(Permissions.Roles.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.Roles.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole
                {
                    Name = model.Name,
                    NameAr = model.NameAr,
                    Description = model.Description
                };
                await _roleManager.CreateAsync(role);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Permissions.Roles.ManagePermissions)]
        public async Task<IActionResult> RolePermissions(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            PermissionViewModel permissionVM = new PermissionViewModel();
            permissionVM.RoleId = id;
            permissionVM.RoleName = role.Name;
            permissionVM.RoleNameAr = role.NameAr;
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
            return View(permissionVM);
        }

        [HttpPost]
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

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Roles.Edit)]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var model = _mapper.Map<RoleViewModel>(role);

            return View(model);
        }

        [Authorize(Permissions.Roles.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                role.Name = model.Name;
                role.NameAr = model.NameAr;
                role.Description = model.Description;

                await _roleManager.UpdateAsync(role);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Archive(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleUsers = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in roleUsers)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            await _roleManager.ArchiveAsync(role);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Unarchive(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            await _roleManager.UnarchiveAsync(role);

            return RedirectToAction(nameof(Index));
        }
    }
}
