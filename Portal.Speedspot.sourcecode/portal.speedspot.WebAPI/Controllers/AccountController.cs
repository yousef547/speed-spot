using portal.speedspot.BizLayer.IdentityModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using portal.speedspot.WebAPI.Helpers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using portal.speedspot.Models.Constants;

namespace portal.speedspot.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ApplicationUserManager _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ApplicationRoleManager _roleManager;
        private IConfiguration _configuration;

        public AccountController(ApplicationUserManager applicationUserManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ApplicationRoleManager roleManager)
        {
            _userManager = applicationUserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null && user.IsArchived == false)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, true);
                if (result.Succeeded)
                {
                    var token = CreateToken(user);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                else if (result.IsLockedOut)
                {
                    var lockoutTimeSpan = user.LockoutEnd - DateTime.UtcNow;
                    ModelState.AddModelError("Email", string.Format(Messages.AccountLockedOut, Math.Ceiling(lockoutTimeSpan.Value.TotalMinutes)));
                }
            }

            ModelState.AddModelError(string.Empty, Messages.InvalidLogin);

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                return Ok(new
                {
                    Token = token
                });
            }
            else
            {
                ModelState.AddModelError("Email", Messages.InvalidLogin);
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        //[HttpPost("ResetPassword")]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //    if (result.Succeeded)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        result = await _userManager.ResetPasswordAsync(user, token, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return Ok(new
        //            {
        //                Message = Messages.PasswordResetSuccessfull
        //            });
        //        }
        //    }
        //    else
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(error.Code, error.Description);
        //        }
        //    }

        //    return BadRequest(new ValidationProblemDetails(ModelState));
        //}

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    Message = Messages.PasswordChangedSuccessfull
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

        [HttpGet("UserRoles")]
        [Authorize]
        public async Task<IActionResult> GetUserRoles()
        {
            List<string> permissions = new List<string>();
            var user = await _userManager.GetUserAsync(User);
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));
            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var rolePermissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                        x.Issuer == "LOCAL AUTHORITY")
                                            .Select(x => x.Value);

                permissions.AddRange(rolePermissions);
            }
            return Ok(new
            {
                Roles = user.UserRoles
            });
        }

        private JwtSecurityToken CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>();
            Claim userIdClaim = new Claim(ClaimTypes.NameIdentifier, user.Id);
            claims.Add(userIdClaim);
            //Avoid Replay attack
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret1"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var encryptingCreds = new EncryptingCredentials(key1, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var handler = new JwtSecurityTokenHandler();
            var t = handler.CreateJwtSecurityToken();
            var token = handler.CreateJwtSecurityToken(
               _configuration["JWT:ValidIssuer"],
               _configuration["JWT:ValidAudience"],
               new ClaimsIdentity(claims),
               expires: DateTime.UtcNow.AddDays(1).ToUniversalTime(),
               signingCredentials: creds,
               encryptingCredentials: encryptingCreds,
               notBefore: DateTime.UtcNow,
               issuedAt: DateTime.UtcNow);

            return token;
        }
    }
}
