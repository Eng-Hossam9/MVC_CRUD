using AutoMapper;
using Company.Hossam.DAL.Models;
using Company.Hossam.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Hossam.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly IMapper _Mapper;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public AccountController(UserManager<ApplicationUser> userManager,IMapper mapper,SignInManager<ApplicationUser> signInManager)
		{
			_UserManager = userManager;
			_Mapper = mapper;
            _SignInManager = signInManager;
        }
        [HttpGet]
		public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userName=await _UserManager.FindByNameAsync(model.UserName);

                if (userName is null)
                {
                    userName = await _UserManager.FindByEmailAsync(model.Email);
                    if (userName is null)
                    {
                        var User = _Mapper.Map<ApplicationUser>(model);
                        var result = await _UserManager.CreateAsync(User, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("SingIn");

                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    ModelState.AddModelError(string.Empty, " UserEmail Already exist");

                }
				ModelState.AddModelError(string.Empty, "UserName Already exist");

			}
			return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn( SignInViewModel model)
       {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _UserManager.FindByEmailAsync(_Mapper.Map<ApplicationUser>(model).Email);
                    if (user is not null)
                    {
                        var Flag = await _UserManager.CheckPasswordAsync(user, model.Password);
                        if (Flag)
                        {
                           var result= await _SignInManager.PasswordSignInAsync(user,model.Password,model.RemembeME,false);

                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(HomeController.Index), "Home");
                            }
                        }

                    }
                    ModelState.AddModelError(string.Empty, "Email Or Password Error");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }

            }
            return View(model);
        }

        

        public new async Task<IActionResult> SignOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }


    }
}
