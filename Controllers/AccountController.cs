﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Roamer.Data;
using Roamer.ViewModels;

namespace Roamer.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        #region Configuration 
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext context;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            context = dbContext;
        }
        #endregion


        #region UserActions

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,

                };
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong Email or Password");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region RoleAction
        [Authorize(Roles = "Admin")]

        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole {

                    Name = model.RoleName

                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList", "Account");
                }
                ModelState.AddModelError("", "Invalid Role");
                return View(model);

            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> EditRole(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);

			EditRoleViewModel model = new EditRoleViewModel
			{

				RoleName = role!.Name,
				RoleId = role.Id

			};
			foreach (var user in _userManager.Users)
			{
				if (await _userManager.IsInRoleAsync(user, role.Name!))
				{
					model.Users.Add(user.Email!);
				}
			}
			return View(model);
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId!);
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList", "Account");
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError(errors.Code, errors.Description);
                }
                return View(model);

            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteRole(string Id, IdentityRole r)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(Id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("RolesList" , "Account");
            }
            return View(role);
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UserRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            List<UserRoleViewModel> userRoleViewModels = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                UserRoleViewModel model = new UserRoleViewModel { 
                
                UserName = user.UserName,
                UserId = user.Id
                
                };

                if (await _userManager.IsInRoleAsync(user,role.Name!))
                {
                   model.IsSelected = true;
                }
                else
                {
                    model.IsSelected= false;
                }

                userRoleViewModels.Add(model);

            }
            return View(userRoleViewModels);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UserRole(string id, List<UserRoleViewModel> models)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            IdentityResult result = null;
            for (int i = 0; i < models.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(models[i].UserId);
                if (models[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(models[i].IsSelected) && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

            }
            if (result!.Succeeded)
            {
                return RedirectToAction("EditRole", new { id = id });

            }
            return View(models);



        }

        #endregion

        #region Dashboard

        [Authorize(Roles ="Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult PlaceDashboard()
        {

            var places = context.Places.ToList();
            return View(places);
        }
        #endregion




    }
}
