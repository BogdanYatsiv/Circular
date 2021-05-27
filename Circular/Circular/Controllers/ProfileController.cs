using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circular.Models.ViewModels;
using Circular.Models;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;
using BLL.Interfaces;
using BLL.Services;

namespace Circular.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private IProjectService _projectService;

        public ProfileController(UserManager<User> userManager, IProjectService projectService)
        {
            _userManager = userManager;
            _projectService = projectService;
        }

        public async Task<IActionResult> Profile()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            ProfileViewModel model = new ProfileViewModel { Username = user.UserName, Email = user.Email };
            ViewBag.Projects = await _projectService.GetProjectsByUserId(user.Id);
            return View(model);
        }

        public async Task<IActionResult> ChangePassword()
        {

            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Користувач не знайдений");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProfileEdit()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            ProfileEditModel model = new ProfileEditModel { Id = user.Id, Email = user.Email, Username = user.UserName};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileEditModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Username;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    } 
}
