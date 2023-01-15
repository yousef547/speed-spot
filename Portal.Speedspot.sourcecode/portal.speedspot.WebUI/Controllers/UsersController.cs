using portal.speedspot.BizLayer.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.WebUI.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmployeeTypesBiz _employeeTypesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly UserSupervisorsBiz _userSupervisorsBiz;
        private readonly EmailSender _mailer;

        public UsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, SignInManager<ApplicationUser> signInManager, EmailSender emailSender
            , IWebHostEnvironment hostEnvironment, IMapper mapper, EmployeeTypesBiz employeeTypesBiz, DepartmentsBiz departmentsBiz,
            UserDepartmentsBiz userDepartmentsBiz, UserSupervisorsBiz userSupervisorsBiz)
            : base(mapper, hostEnvironment, userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mailer = emailSender;
            _employeeTypesBiz = employeeTypesBiz;
            _departmentsBiz = departmentsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _userSupervisorsBiz = userSupervisorsBiz;
        }

        [Authorize(Permissions.Users.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.Include(x => x.EmployeeType).Include(x => x.Department).Select(u => _mapper.Map<UserViewModel>(u));
            return PartialView("_ShowUsersPartial", await users.ToListAsync());
        }


        [Authorize(Permissions.Users.Create)]
        public async Task<IActionResult> Create()
        {
            ViewData["EmployeeTypeId"] = await _employeeTypesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            ViewData["DirectManagerId"] = await _userManager.GetAllUsersAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Permissions.Users.Create)]
        public async Task<IActionResult> Create(CreateUserViewModel model, string actionType = "Add")
        {
            if (ModelState.IsValid)
            {
                string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                var user = _mapper.Map<ApplicationUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.SupervisorIds != null)
                    {
                        foreach (string supervisorId in model.SupervisorIds)
                        {
                            await _userSupervisorsBiz.AddAsync(new UserSupervisor
                            {
                                SupervisorId = supervisorId,
                                UserId = user.Id
                            });
                        }
                    }

                    user = await _userManager.FindByEmailAsync(model.Email);

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: Request.Scheme);
                    string message = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";
                    string messageBody = BuildEmailTemplate("Confirm_Account_Create.html", "Welcome Email", model.FirstName + " " + model.LastName, model.Email, model.Password, message, baseUrl);
                    _mailer.Send("Email Confirmation", messageBody, user.Email, user.UserName);

                    if (model.ProfileImage != null)
                    {
                        string uniqueFileName = UploadFile(model.ProfileImage, "Users");
                        await _userManager.UpdateProfilePicture(user, uniqueFileName);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            ViewData["EmployeeTypeId"] = await _employeeTypesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            ViewData["DirectManagerId"] = await _userManager.GetAllUsersAsync();
            return View(model);
        }

        [Authorize(Permissions.Users.Edit)]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.GetByIdAsync(id);

            var model = _mapper.Map<EditUserViewModel>(user);

            ViewData["EmployeeTypeId"] = await _employeeTypesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            ViewData["DirectManagerId"] = await _userManager.GetAllUsersExceptAsync(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Users.Edit)]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool emailChanged = false;
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user.Email != model.Email)
                {
                    emailChanged = true;
                    user.EmailConfirmed = false;
                }

                user.Email = model.Email;
                user.UserName = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.EmployeeTypeId = model.EmployeeTypeId;
                user.DepartmentId = model.DepartmentId;
                user.DirectManagerId = model.DirectManagerId;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userSupervisorsBiz.DeleteByUserAsync(user.Id);
                    if (model.SupervisorIds != null)
                    {
                        foreach (string supervisorId in model.SupervisorIds)
                        {
                            await _userSupervisorsBiz.AddAsync(new UserSupervisor
                            {
                                SupervisorId = supervisorId,
                                UserId = user.Id
                            });
                        }
                    }

                    if (emailChanged)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: Request.Scheme);
                        string message = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";
                        string messageBody = BuildEmailTemplate("Confirm_Account_Edit.html", "Email Confirmation", model.FirstName + " " + model.LastName, message);
                        _mailer.Send("Email Confirmation", messageBody, user.Email, user.UserName);
                    }

                    if (model.ProfileImage != null)
                    {
                        string uniqueFileName = UploadFile(model.ProfileImage, "Users");
                        await _userManager.UpdateProfilePicture(user, uniqueFileName);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            ViewData["EmployeeTypeId"] = await _employeeTypesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            ViewData["DirectManagerId"] = await _userManager.GetAllUsersExceptAsync(model.Id);
            return View(model);
        }

        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Archive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.ArchiveAsync(user);

            return Json(result);
        }

        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Unarchive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.UnarchiveAsync(user);

            return Json(result);
        }

        [Authorize(Permissions.Users.ManageRoles)]
        public async Task<IActionResult> UserRoles(string id)
        {
            var viewModel = new List<RolesViewModel>();
            var user = await _userManager.FindByIdAsync(id);

            foreach (var role in _roleManager.Roles.Where(r => r.IsArchived == false).ToList())
            {
                var userRolesViewModel = new RolesViewModel
                {
                    RoleName = role.Name,
                    RoleNameAr = role.NameAr
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

            return View(model);
        }

        [Authorize(Permissions.Users.ManageRoles)]
        public async Task<IActionResult> UpdateRoles(UserRolesViewModel userRolesViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRolesViewModel.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, userRolesViewModel.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Users.ManageDepartments)]
        public async Task<IActionResult> UserDepartments(string id)
        {
            var viewModel = new List<DepartmentsViewModel>();
            var user = await _userManager.FindByIdAsync(id);

            foreach (var department in await _departmentsBiz.GetAllUnarchivedAsync())
            {
                var userDepartmentsViewModel = new DepartmentsViewModel
                {
                    DepartmentName = department.Name,
                    DepartmentNameAr = department.NameAr,
                    DepartmentId = department.Id
                };
                if (await _userDepartmentsBiz.IsInDepartmentAsync(id, department.Id))
                {
                    userDepartmentsViewModel.Selected = true;
                }
                else
                {
                    userDepartmentsViewModel.Selected = false;
                }
                viewModel.Add(userDepartmentsViewModel);
            }

            var model = new UserDepartmentsViewModel()
            {
                UserId = id,
                Username = user.UserName,
                UserDepartments = viewModel
            };

            return View(model);
        }

        [Authorize(Permissions.Users.ManageDepartments)]
        public async Task<IActionResult> UpdateDepartments(UserDepartmentsViewModel userDepartmentsViewModel)
        {
            var user = await _userManager.FindByIdAsync(userDepartmentsViewModel.UserId);
            await _userDepartmentsBiz.DeleteByUserAsync(userDepartmentsViewModel.UserId);
            foreach (var selectedDepartment in userDepartmentsViewModel.UserDepartments.Where(x => x.Selected))
            {
                await _userDepartmentsBiz.AddAsync(new UserDepartment
                {
                    UserId = userDepartmentsViewModel.UserId,
                    DepartmentId = selectedDepartment.DepartmentId
                });
            }

            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Users.ResetPassword)]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = Url.Action("ResetPassword", "Account", new { token = token }, protocol: Request.Scheme);
                _mailer.Send("Reset Password", $"Please reset your account password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", user.Email, user.UserName);
            }

            return View();
        }

        [Authorize(Permissions.Users.SendConfirmationEmail)]
        public async Task<IActionResult> SendConfirmationEmail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: Request.Scheme);
                string message = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";
                string messageBody = BuildEmailTemplate("Confirm_Account_Create.html", "Welcome Email", user.FirstName + " " + user.LastName, user.Email, "", message, baseUrl);
                _mailer.Send("Email Confirmation", messageBody, user.Email, user.UserName);
            }

            return View();
        }

        [Authorize(Permissions.Users.Details)]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.GetByIdAsync(id);

            var model = _mapper.Map<UserInfoViewModel>(user);

            return PartialView("_UserDetailsPartial", model);
        }
    }
}
