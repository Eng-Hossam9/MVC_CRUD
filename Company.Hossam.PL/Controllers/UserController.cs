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
using System.Data;

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
            var userList = Enumerable.Empty<ApplicationUser>();

            if (string.IsNullOrEmpty(searchInput))
            {
                userList = await _UserManager.Users.ToListAsync();
            }
            else
            {
                userList = await _UserManager.Users
                    .Where(u => u.Email.ToLower().Contains(searchInput.ToLower()))
                    .ToListAsync();
            }

            var userViewModels = new List<UserViewModel>();

            foreach (var user in userList)
            {
                var roles = await _UserManager.GetRolesAsync(user); 

                userViewModels.Add(new UserViewModel()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = roles
                });
            }

            return View(userViewModels); // Return the view with the list of user view models
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
