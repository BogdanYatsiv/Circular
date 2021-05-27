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

        public ProjectController(ApplicationDbContext context, UserManager<User> _userManager, IProjectService _projectService)
        {
            dbContext = context;
            userManager = _userManager;
            projectService = _projectService;
        }

        [HttpPost]
        public async Task<IActionResult> Project()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Project(string pr, string GithubLink)
        {
            var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(pr.ToString());

            string JsonRequestResultCommits;

            List<CommitResponse> commits = new List<CommitResponse>();

            var toFormat = GithubLink.Split("/");
            string apiUrl = string.Format("https://api.github.com/repos/{0}/{1}/commits",
                toFormat[toFormat.Count() - 2], toFormat[toFormat.Count() - 1]);

            //Request body
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            //UserAgent required for GitHub API
            request.UserAgent = "Google Chrome 40 (Win 8.1 x64): Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.115 Safari/537.36";
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    JsonRequestResultCommits = reader.ReadToEnd();
                }
            }
            response.Close();

            //JsonRequestResult is an array of json nums
            JArray jR = JArray.Parse(JsonRequestResultCommits);

            foreach (JObject value in jR)
            {
                CommitResponse commitResponse = JsonConvert.DeserializeObject<CommitResponse>(value.ToString());
                commits.Add(commitResponse);
            }
            ViewBag.Commits = commits;
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
            User user = await userManager.GetUserAsync(HttpContext.User);

            string JsonRequestResult;
            ProjectResponse projectResponse = new ProjectResponse();
            var toFormat = model.GithubLink.Split("/");
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

            string s = JsonConvert.SerializeObject(projectResponse);
            //TO DO: заносити проект в базу даних
            Project project = new Project
            {
                githubLink = projectResponse.url,
                name = projectResponse.name,
                language = projectResponse.language,
                createDate = projectResponse.created_at,
                UserId = user.Id
            };
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();
            
            if (model.GithubLink.Length >= 0)
            {
                return RedirectToAction("Project", new { pr = s.ToString(), GithubLink = model.GithubLink });
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LookProject(int projectId)
        {
            Project project = await projectService.FindProject(projectId);

            User user = await userManager.GetUserAsync(HttpContext.User);

            string JsonRequestResultProject;

            ProjectResponse projectResponse = new ProjectResponse();

            var toFormat = project.githubLink.Split("/");
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
                    JsonRequestResultProject = reader.ReadToEnd();
                }
            }
            response.Close();

            JObject jO = JObject.Parse(JsonRequestResultProject);
            projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(jO.ToString());

            string s = JsonConvert.SerializeObject(projectResponse);

            if (project.githubLink.Length >= 0)
            {
                return RedirectToAction("Project", new { pr = s.ToString() , GithubLink = project.githubLink});
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int projectId)
        {
            Project project = await projectService.FindProject(projectId);
            ProjectViewModel model = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.name,
                Language = project.language,
                GithubLink = project.githubLink
            };
            return View("Delete", model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProject(ProjectViewModel model)
        {
            await projectService.DeleteProject(model.Id);
            return RedirectToAction("Profile", "Profile");
        }
    }
}
