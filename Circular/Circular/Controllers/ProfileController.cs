using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circular.Models.ViewModels;
using Circular.Models;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;

namespace Circular.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        

        //private readonly 
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ProfileEdit(ProfileEditModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> ChangePassword()
        {

            User user = await _userManager.FindByNameAsync(User.Identity.Name);
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

                User user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
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
    } 
}
