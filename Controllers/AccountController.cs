using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Re.DAL.Models;
using Re.PL.Helpers;
using Re.PL.ViewModels;

namespace Re.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager <ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
            public async Task<IActionResult> SignUp(SingUpVM model)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.FullName,
                        Email = model.Email,
                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmEmailLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

                        var email = new Email()
                        {
                            Subject = "Confirm Email",
                            Recivers = model.Email,
                            Body = $"Please confirm your email, click: {confirmEmailLink}"
                        };

                        EmailSetting.SendEmail(email);
                        return View(nameof(SignIn));
                    }
                }

                return View();
            }

            public async Task<IActionResult> ConfirmEmail(string userId, string token)
            {
                if (userId == null || token == null)
                {
                    return NotFound();
                }

                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var result = await userManager.ConfirmEmailAsync(user, token);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                }

                return NotFound();
            }


            [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> SignIn(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user  is not null)
                {
                    var check = await userManager.CheckPasswordAsync(user , model.Password);
                    if (check)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction
                               ("Index", "Home");
                        }
                    }
                }

            }

                return View(model);
        }


       public IActionResult ForgetPassword()
        {
            return View();

        }

        public async Task<IActionResult> SendResetPassURL(ForgetPasswordVM model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPassUrl = Url.Action("ResetPassword" , "Account" , new {email=model.Email , token=Token } ,"https" , "localhost:7134");
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        Recivers = model.Email,
                        Body = ResetPassUrl,
                    };
                    EmailSetting.SendEmail(email);
                }
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult ResetPassword(string email,string token )
        {
            return View();

        }
        [HttpPost]
        public async Task <IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is not null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
            }

            return View(model);

        }







    }
}
