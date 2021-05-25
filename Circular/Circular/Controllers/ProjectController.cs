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
using Octopus.Client.Repositories;
using BLL.Services;
using NUnit.Util;

namespace Circular.Controllers
{
    public class ProjectController : Controller
    {
        //private ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private ProjectService projectService;

        public ProjectController(ApplicationDbContext context, UserManager<User> _userManager, ProjectService _projectService)
        {
            //dbContext = context;
            userManager = _userManager;
            projectService = _projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Project(string GithubLink)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);

            string JsonRequestResult;
            ProjectResponse projectResponse = new ProjectResponse();
            var toFormat = GithubLink.Split("/");
            string apiUrl = string.Format("https://api.github.com/repos/{0}/{1}",
                toFormat[toFormat.Length - 2], toFormat[toFormat.Length - 1]);

            //Request body
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            //UserAgent required for GitHub API
            request.UserAgent = "Google Chrome 40 (Win 8.1 x64): Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.115 Safari/537.36";
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    JsonRequestResult = reader.ReadToEnd();
                }
            }
            response.Close();

            JObject jO = JObject.Parse(JsonRequestResult);
            projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(jO.ToString());

            //TO DO: заносити проект в базу даних
            Project project = new Project { githubLink = projectResponse.url, name = projectResponse.name, 
                language = projectResponse.language, createDate = projectResponse.created_at, UserId = user.Id};
            //dbContext.Projects.Add(project);
            //dbContext.SaveChanges();
            var projects = await projectService.Get
            return View(projectResponse);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectModel model)
        {
            if(model.GithubLink.Length >= 0)
            {
                return RedirectToAction("Project", "Project", new { model.GithubLink });
            }

            return View();
        }
    }
}
