using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Circular.Models.JsonModels;
using Circular.Models.ViewModels;
using DAL.Entities;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using BLL.Services;
using BLL.Interfaces;
using System;

namespace Circular.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private IProjectService projectService;
        private ISubprojectService subprojectService;

        public ProjectController(ApplicationDbContext context, UserManager<User> _userManager, IProjectService _projectService, ISubprojectService _subprojectService)
        {
            dbContext = context;
            userManager = _userManager;
            projectService = _projectService;
            subprojectService = _subprojectService;
        }


        //PROJECT
        [HttpGet]
        public async Task<IActionResult> Project(int id)
        {
            var pr = await projectService.FindProjectById(id);
            var model = new ProjectViewModel { Id = pr.Id, Name = pr.name };
            ViewBag.Subprojects = await subprojectService.GetSubprojectsByProjectId(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectModel model)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);

            
            Project project = new Project
            {
                name = model.Name,
                UserId = user.Id
            };
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();

            var id = dbContext.Projects.Where(x => x.name == project.name).First().Id;

            ProjectViewModel projectViewModel = new ProjectViewModel { Id = id, Name = project.name};

            return RedirectToAction("Project", "Project", projectViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LookProject(int id)
        {
            Project project = await projectService.FindProjectById(id);

            if (project.name.Length >= 0)
            {
                return RedirectToAction(nameof(Project), new { id = id });
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteProject(int id)
        {
            Project project = await projectService.FindProjectById(id);
            ProjectViewModel model = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.name,
            };
            return View("DeleteProject", model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProject(ProjectViewModel model)
        {
            await projectService.DeleteProject(model.Id);
            return RedirectToAction("Profile", "Profile");
        }




    }
}
