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

		public AccountController(UserManager<ApplicationUser> userManager,IMapper mapper)
		{
			_UserManager = userManager;
			_Mapper = mapper;
		}
		public IActionResult SignUp()
        {
            return View();
        }

        public async Task<IActionResult> Signin(SignUpViewModel model)
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

                    ModelState.AddModelError(string.Empty, "User Already exist");

                }
				ModelState.AddModelError(string.Empty, "User Already exist");

			}
			return View(model);
        }
    }
}
