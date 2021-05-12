using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circular.Models.ViewModels;
using DAL.Entities;

namespace Circular.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Project()
        {
            return View();
        }

        public IActionResult CreateProject(CreateProjectModel model)
        {
            return View(model);
        }
    }
}
