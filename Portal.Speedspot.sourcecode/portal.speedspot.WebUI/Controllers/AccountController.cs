using portal.speedspot.BizLayer.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using AutoMapper;

namespace portal.speedspot.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmailSender _mailer;

        public AccountController(ApplicationUserManager userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment hostEnvironment,
            EmailSender emailSender,
            LocalizationService localizationService,
            IMapper mapper)
            : base(mapper, hostEnvironment, userManager, localizationService)
        {
            _signInManager = signInManager;
            _mailer = emailSender;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                }, model.Password);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, protocol: Request.Scheme);

                    _mailer.Send("Email Confirmation", $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.", user.Email, user.UserName);
                    string uniqueFileName = UploadFile(model.ProfileImage, "Users");
                    user.ProfilePicture = uniqueFileName;
                    await _userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(RegisterConfirmation), new { email = model.Email });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult RegisterConfirmation(string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            List<string> errors = new();
            if (user != null && token != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        errors.Add(error.Description);
                }
            }
            else
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            ViewBag.Errors = errors;

            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, bool remember, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && user.IsArchived)
                {
                    ModelState.AddModelError("", _localizationService["LoginNotAllowed"].Value);
                }
                else
                {
                    var signInresult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, remember, true);
                    if (signInresult == Microsoft.AspNetCore.Identity.SignInResult.Success)
                    {
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    else if (signInresult == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                    {
                        ModelState.AddModelError("", _localizationService["InvalidLoginAttempt"].Value);
                    }
                    else if (signInresult == Microsoft.AspNetCore.Identity.SignInResult.LockedOut)
                    {
                        var minutes = (user.LockoutEnd - DateTime.UtcNow).Value.Minutes;
                        ModelState.AddModelError("", string.Format(_localizationService["AccountInLockout"].Value, minutes));
                    }
                    else if (signInresult == Microsoft.AspNetCore.Identity.SignInResult.NotAllowed)
                    {
                        if (!user.EmailConfirmed)
                        {
                            ModelState.AddModelError("Email", _localizationService["EmailNotConfirmed"].Value);
                        }
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { token }, protocol: Request.Scheme);
                    _mailer.Send("Forgot Password", $"Please reset your account password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", user.Email, user.UserName);
                }

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token)
        {
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            ResetPasswordViewModel model = new()
            {
                Code = token
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(ResetPasswordConfirmation));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This account doesn't exist");
                }
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetByEmailAsync(User.Identity.Name);
            var model = _mapper.Map<EditProfileViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userManager.GetByEmailAsync(User.Identity.Name));
                if (model.ProfileImage != null)
                {
                    if (user.ProfilePicture != null)
                    {
                        DeleteFile("Users", user.ProfilePicture);
                    }
                    string uniqueFileName = UploadFile(model.ProfileImage, "Users");
                    user.ProfilePicture = uniqueFileName;
                }
                //user = _mapper.Map<ApplicationUser>(model);
                user.PhoneNumber = model.PhoneNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(model);

        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userManager.GetByEmailAsync(User.Identity.Name));
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);


        }
    }
}

//Yo@2012

