using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using CoWorker.Models.Identity;
using CoWorker.Models.Identity.Manages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentitySample.Controllers;

namespace IdentitySamples.Controllers
{
    [Controller]
    [Authorize]
    public class ManageController
    {
        public const string MANAGE = "Manage";
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IActionContextAccessor _accessor;
        private readonly object Empty = new { };
        private readonly IUrlHelper _url;

        public ManageController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory,
        IActionContextAccessor accessor,
        IUrlHelper url)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            this._accessor = accessor;
            this._url = url;
        }

        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            var msg =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = await GetCurrentUserAsync();
            var model = new Index
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                AuthenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user),
                MessageType = message?.ToString(),
                Message = msg
            };
            return new OkObjectResult(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        
        public async Task<IActionResult> RemoveLogin(RemoveLogin account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return new RedirectToActionResult(nameof(ManageLogins), MANAGE, new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public IActionResult AddPhoneNumber()
        {
            return new OkResult();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumber model)
        {
            if (!_accessor.ActionContext.ModelState.IsValid)
            {
                return new OkObjectResult(model);
            }
            // Generate the token and send it
            var user = await GetCurrentUserAsync();
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
            return new RedirectToActionResult(nameof(VerifyPhoneNumber), MANAGE, new { PhoneNumber = model.PhoneNumber });
        }

        //
        // POST: /Manage/ResetAuthenticatorKey
        [HttpPost]
        
        public async Task<IActionResult> ResetAuthenticatorKey()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                _logger.LogInformation(1, "User reset authenticator key.");
            }
            return new RedirectToActionResult(nameof(Index), MANAGE, Empty);
        }

        //
        // POST: /Manage/GenerateRecoveryCode
        [HttpPost]
        
        public async Task<IActionResult> GenerateRecoveryCode()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var codes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
                _logger.LogInformation(1, "User generated new recovery code.");
                return new OkObjectResult(new DisplayRecoveryCodes { Codes = codes });
            }
            return new BadRequestResult();
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return new RedirectToActionResult(nameof(Index), MANAGE, Empty);
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return new RedirectToActionResult(nameof(Index), MANAGE,Empty);
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(await GetCurrentUserAsync(), phoneNumber);
            // Send an SMS to verify the phone number
            return phoneNumber == null ? new OkObjectResult("Error") : new OkObjectResult(new VerifyPhoneNumber { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumber model)
        {
            if (!_accessor.ActionContext.ModelState.IsValid)
            {
                return new OkObjectResult(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new RedirectToActionResult(nameof(Index),MANAGE, new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            _accessor.ActionContext.ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return new BadRequestObjectResult(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        [HttpPost]
        
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new RedirectToActionResult(nameof(Index),MANAGE, new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return new BadRequestObjectResult(new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return new OkResult();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!_accessor.ActionContext.ModelState.IsValid)
            {
                return new BadRequestObjectResult(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return new RedirectToActionResult(nameof(Index),MANAGE, new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return new BadRequestObjectResult(model);
            }
            return new BadRequestObjectResult(new { Message = ManageMessageId.Error });
        }
        //
        // POST: /Manage/SetPassword
        [HttpPost]
        
        public async Task<IActionResult> SetPassword(SetPassword model)
        {
            if (!_accessor.ActionContext.ModelState.IsValid)
            {
                return new BadRequestObjectResult(model);
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new OkObjectResult(new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return new BadRequestObjectResult(model);
            }
            return new BadRequestObjectResult(new { Message = ManageMessageId.Error });
        }

        //GET: /Manage/ManageLogins
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            var msg =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return new OkObjectResult("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var otherLogins = schemes.Where(auth => userLogins.All(ul => auth.Name != ul.LoginProvider)).ToList();
            return new OkObjectResult(new ManageLogins
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins,
                MessageType = message?.ToString(),
                Message = msg
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = _url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                provider, 
                redirectUrl, 
                _userManager.GetUserId(_accessor.ActionContext.HttpContext.User));
            return new ChallengeResult(provider,properties);
        }

        //
        // GET: /Manage/LinkLoginCallback
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return new BadRequestResult();
            }
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return new BadRequestObjectResult(new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return new OkObjectResult(new { Message = message });
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _accessor.ActionContext.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(_accessor.ActionContext.HttpContext.User);
        }

        #endregion
    }
}
