using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.WebAPI.Infrastracture;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
            : base(mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Authorize(Permissions.Users.View)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.GetAllUsersAsync();
            var userVMs = users.Select(u => _mapper.Map<UserViewModel>(u)).ToList();
            return Ok(userVMs);
        }

        [Authorize(Permissions.Users.ViewArchived)]
        [HttpGet("Archive")]
        public async Task<IActionResult> GetAllArchived()
        {
            var users = await _userManager.GetAllUsersAsync(true);
            var userVMs = users.Select(u => _mapper.Map<UserViewModel>(u)).ToList();
            return Ok(userVMs);
        }

        [HttpGet("{id}")]
        [Authorize(Permissions.Users.Details)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var viewModel = _mapper.Map<UserViewModel>(user);
                return Ok(viewModel);
            }
            return NotFound();
        }

        [HttpPost("")]
        [Authorize(Permissions.Users.Create)]
        public async Task<IActionResult> Post(CreateUserViewModel model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
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

        [Authorize(Permissions.Users.Edit)]
        [HttpPut("")]
        public async Task<IActionResult> Put(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.ProfilePicture = model.ProfilePicture;

            var result = await _userManager.UpdateAsync(user);
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
        [Authorize(Permissions.Users.Delete)]
        public async Task<IActionResult> Archive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.IsArchived = true;
            await _userManager.ArchiveAsync(user);

            return Ok(new
            {
                Message = Messages.ArchiveSuccessfull
            });
        }

        [HttpPut("Unarchive/{id}")]
        [Authorize(Permissions.Users.Delete)]
        public async Task<IActionResult> Unarchive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.IsArchived = true;
            await _userManager.UnarchiveAsync(user);

            return Ok(new
            {
                Message = Messages.UnarchiveSuccessfull
            });
        }

        [Authorize(Permissions.Users.ManageRoles)]
        [HttpGet("{id}/Roles")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var viewModel = new List<RolesViewModel>();
            var user = await _userManager.FindByIdAsync(id);

            var roles = await _roleManager.GetAllAsync();
            foreach (var role in roles)
            {
                var userRolesViewModel = new RolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }

            var model = new UserRolesViewModel()
            {
                UserId = id,
                Username = user.UserName,
                UserRoles = viewModel
            };

            return Ok(model);
        }

        [Authorize(Permissions.Users.ManageRoles)]
        [HttpPut("{id}/Roles")]
        public async Task<IActionResult> UpdateRoles(UserRolesViewModel userRolesViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRolesViewModel.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, userRolesViewModel.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            return Ok(new
            {
                Message = Messages.UpdateSuccessfull
            });
        }
    }
}
