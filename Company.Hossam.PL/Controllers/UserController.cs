using AutoMapper;
using Company.Hossam.BLL.InterFaces;
using Company.Hossam.BLL.Repositories;
using Company.Hossam.DAL.Model;
using Company.Hossam.DAL.Models;
using Company.Hossam.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Company.Hossam.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {


        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly IMapper _Mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManger)
        {

            _UserManager = userManager;
            _Mapper = mapper;
            _RoleManger = roleManger;
        }


        public async Task<IActionResult> Index(string searchInput = "")
        {
            var user = Enumerable.Empty<UserViewModel>();

            if (searchInput.IsNullOrEmpty())
            {
                user = await _UserManager.Users.Select(u => new UserViewModel()
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Id = u.Id,
                    UserName = u.UserName,
                    Roles = _UserManager.GetRolesAsync(u).Result
                }).ToListAsync();

            }
            else
            {
                user = await _UserManager.Users.Where(U => U.Email.ToLower().Contains(searchInput.ToLower())).Select(u => new UserViewModel()
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Id = u.Id,
                    UserName = u.UserName,
                    Roles = _UserManager.GetRolesAsync(u).Result
                }).ToListAsync();

            }

            return View(user);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _UserManager.FindByIdAsync(id);

            var result = await _UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }


            return BadRequest();

        }





        public async Task<IActionResult> Details([FromRoute] string id)
        {
            if (id == null) return BadRequest();
            var Employee = await _UserManager.FindByIdAsync(id);

            var user = new UserViewModel()
            {
                Email = Employee.Email,
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                Id = Employee.Id,
                UserName = Employee.UserName,
                Roles = _UserManager.GetRolesAsync(Employee).Result

            };
            return View(user);
        }




        public async Task<IActionResult> Update(string? id)
        {
            if (id == null) return BadRequest();
            var User = await _UserManager.FindByIdAsync(id);

            if (User is null)
            {
                return NotFound();
            }
            var UserViewModel = new UserViewModel()
            {
                UserName = User.UserName,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Id = User.Id,
                Roles = _UserManager.GetRolesAsync(User).Result
            };

            return View(UserViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id, UserViewModel userViewModel)
        {
            try
            {
                if (id != userViewModel.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var user = await _UserManager.FindByIdAsync(id);

                    if (user is not null)
                    {
                        user.Email = userViewModel.Email;
                        user.FirstName = userViewModel.FirstName;
                        user.LastName = userViewModel.LastName;

                        var result = await _UserManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");

                        }
                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return View(userViewModel);


        }



    }
}
