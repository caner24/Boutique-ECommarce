using AutoMapper;
using Boutique.Business.Abstract;
using Boutique.Entities.Concrate;
using Boutique.MailService;
using Boutique.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boutique.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IVisitService _visitService;
        public AccountController(IVisitService visitService, IMemoryCache cache, IEmailSender emailSender, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _visitService = visitService;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalRegister(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalRegisterCallBack), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (signInResult.IsLockedOut)
            {
                return RedirectToAction(nameof(ForgotPassword));
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["Provider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                ModelState.AddModelError("", "Böyle bir kullanici bulunamadi !.");
                return View("Login");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ExternalRegisterCallBack(string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Zaten böyle bir kullanici mevcut !.");
                return View("Register");
            }
            if (signInResult.IsLockedOut)
            {
                return RedirectToAction(nameof(ForgotPassword));
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                ExternalLoginModel userModel = new ExternalLoginModel();
                userModel.Email = email;
                userModel.Principal = info.Principal;
                User user = new User();
                user = _mapper.Map<User>(userModel);
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _userManager.ConfirmEmailAsync(user, token);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.TryAddModelError(item.Code, item.Description);
                    }
                    return View("Register");
                }
            }
            return View("Register");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "E-Posta Adresinizi Doğrulayın", EmailBodyViewModel.MailBodyConfirmation(user.Email.Substring(0, user.Email.IndexOf("@")), "Email Onaylama", "2 hour", confirmationLink.ToString()), null);
            await _emailSender.SendEmailAsync(message);
            await _userManager.AddToRoleAsync(user, "Visitor");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Redirect(result.Succeeded ? "/" : "Home/Error");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var result = await _signInManager.PasswordSignInAsync(userModel.Email.Substring(0, userModel.Email.IndexOf("@")), userModel.Password, userModel.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var signedUser = await _userManager.FindByEmailAsync(userModel.Email);
                HttpClient client = new HttpClient();
                var datas = await client.GetFromJsonAsync<Visits>("http://ip-api.com//json");
                datas.UserId = signedUser.Id;
                await _visitService.Add(datas);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                if (result.IsLockedOut)
                {
                    TempData["error"] = $"Hesabiniz yanlış şifre denemeleri nedeniyle dondurulmuştur.2 dakika içinde tekrar deneyiniz";
                    TempData["color"] = "danger";
                }
                else
                {
                    if (result.IsNotAllowed)
                    {
                        TempData["error"] = $"Lütfen email adresinizi doğrulayiniz.";
                        TempData["color"] = "danger";
                    }
                    else
                    {
                        TempData["error"] = $"Şifreniz veya mail adresiniz hatali.";
                        TempData["color"] = "danger";
                    }

                }

                return View();
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return Redirect("/");
        }
        private async Task<bool> IsTokenValid(object type, string tokenType, string userMail, string token)
        {
            var user = await _userManager.FindByEmailAsync(userMail);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.VerifyUserTokenAsync(user, (string)type, tokenType, token);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                TempData["error"] = "Böyle Bir Kullanici Bulunamadi";
                TempData["color"] = "danger";
                return RedirectToAction("Login", "Account");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Şifre Sıfırlama", EmailBodyViewModel.MailBody(user.Email.Substring(0, user.Email.IndexOf("@")), "Şifre Sıfırlama", "2 hour", callback.ToString()), null);
            await _emailSender.SendEmailAsync(message);
            TempData["error"] = "Şifre Sifirlama Linki Gönderildi";
            TempData["color"] = "success";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (await IsTokenValid(_userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", user.Email, resetPasswordModel.Token))
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    return View();
                }
                else
                {
                    TempData["error"] = "Şifreniz başarili bir şekilde sifirlandı";
                    TempData["color"] = "success";
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                TempData["error"] = "Bozuk Bağlanti Veya Geçerliliği Geçmiş !.";
                TempData["color"] = "danger";
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
