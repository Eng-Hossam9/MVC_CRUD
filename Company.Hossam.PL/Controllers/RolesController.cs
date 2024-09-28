using AutoMapper;
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
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper, RoleManager<IdentityRole> roleManger, UserManager<ApplicationUser> userManager)
        {

            _RoleManger = roleManger;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string searchInput = "")
        {
            var roles = Enumerable.Empty<RoleViewModel>();

            if (searchInput.IsNullOrEmpty())
            {
                roles = await _RoleManger.Roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();

            }
            else
            {
                roles = await _RoleManger.Roles.Where(r => r.Name.ToLower().Contains(searchInput.ToLower())).Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();

            }
            return View(roles);
        }



        public IActionResult Create()
        {


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel RoleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var role = new IdentityRole()
                    {

                        Name = RoleViewModel.Name
                    };
                    var result = await _RoleManger.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");

                    }
                }

            }


            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return View(RoleViewModel);


        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _RoleManger.FindByIdAsync(id);

            var result = await _RoleManger.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }


            return BadRequest();
        }





        public async Task<IActionResult> Details([FromRoute] string id)
        {
            if (id == null) return BadRequest();
            var role = await _RoleManger.FindByIdAsync(id);

            var roles = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name

            };
            return View(roles);
        }




        public async Task<IActionResult> Update(string? id)
        {
            if (id == null) return BadRequest();
            var role = await _RoleManger.FindByIdAsync(id);

            if (role is null)
            {
                return NotFound();
            }
            var roleViewModel = new RoleViewModel()
            {
                Name = role.Name,

                Id = role.Id,
            };

            return View(roleViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id, RoleViewModel RoleViewModel)
        {
            try
            {
                if (id != RoleViewModel.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var role = await _RoleManger.FindByIdAsync(id);

                    if (role is not null)
                    {

                        role.Id = RoleViewModel.Id;
                        role.Name = RoleViewModel.Name;

                        var result = await _RoleManger.UpdateAsync(role);

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


            return View(RoleViewModel);


        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveRoles(string id)
        {
            var role = await _RoleManger.FindByIdAsync(id);
            if (role is null) return NotFound();

            var usersInRole = new List<AssignRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userinrole = new AssignRoleViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName

                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userinrole.IsChecked = true;
                }
                else
                {
                    userinrole.IsChecked = false;
                }

                usersInRole.Add(userinrole);
            }

            ViewData["RoleName"] = role.Name;
            ViewData["RoleId"] = role.Id;


            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveRoles(string id, List<AssignRoleViewModel> assignRoleViewModels)
        {
            var role = await _RoleManger.FindByIdAsync(id);
            if (id is null) return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var user in assignRoleViewModels)
                {
                    var appuser = await _userManager.FindByIdAsync(user.Id);


                    if (user.IsChecked == true && ! await _userManager.IsInRoleAsync(appuser , role.Name))
                    {
                        await _userManager.AddToRoleAsync(appuser, role.Name);

                    }
                    else if (! user.IsChecked == true && await _userManager.IsInRoleAsync(appuser, role.Name))
                    {
                         await _userManager.RemoveFromRoleAsync(appuser,role.Name);

                    }

                }
                return RedirectToAction(nameof(Index));
            }
                return View(assignRoleViewModels);



        }
    }
}
