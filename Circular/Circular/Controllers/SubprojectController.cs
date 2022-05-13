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
    public class SubprojectController : Controller
    {
        private ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private IProjectService projectService;
        private ISubprojectService subprojectService;
        

        public SubprojectController(ApplicationDbContext context, UserManager<User> _userManager, ISubprojectService _subprojectService, IProjectService _projectService)
        {
            dbContext = context;
            userManager = _userManager;
            projectService = _projectService;
            subprojectService = _subprojectService;
        }


  
        //SUBPROJECT
        [HttpPost]
        public async Task<IActionResult> Subproject()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Subproject(string pr, string GithubLink)
        {
            var projectResponse = JsonConvert.DeserializeObject<SubprojectResponse>(pr.ToString());

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
        public IActionResult CreateSubproject(int id)
        {
            return View(new CreateSubprojectModel { ProjectId = id } );
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubproject(CreateSubprojectModel model)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);

            string JsonRequestResult;
            SubprojectResponse subprojectResponse = new SubprojectResponse();
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

            subprojectResponse = JsonConvert.DeserializeObject<SubprojectResponse>(jO.ToString());

            string s = JsonConvert.SerializeObject(subprojectResponse);
            //TO DO: заносити проект в базу даних
            Subproject subproject = new Subproject
            {
                githubLink = subprojectResponse.url,
                name = subprojectResponse.name,
                language = subprojectResponse.language,
                createDate = subprojectResponse.created_at,
                ProjectId = model.ProjectId
            };
            dbContext.Subprojects.Add(subproject);
            dbContext.SaveChanges();
            
            if (model.GithubLink.Length >= 0)
            {
                return RedirectToAction("Subproject", new { pr = s.ToString(), GithubLink = model.GithubLink });
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LookSubproject(int subprojectId)
        {
            Subproject subproject = await subprojectService.FindProject(subprojectId);

            //User user = await userManager.GetUserAsync(HttpContext.User);

            string JsonRequestResultProject;

            SubprojectResponse projectResponse = new SubprojectResponse();

            var toFormat = subproject.githubLink.Split("/");
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
            projectResponse = JsonConvert.DeserializeObject<SubprojectResponse>(jO.ToString());

            string s = JsonConvert.SerializeObject(projectResponse);

            if (subproject.githubLink.Length >= 0)
            {
                //return RedirectToAction("Subproject", new { pr = s.ToString() , GithubLink = subproject.githubLink});
                return RedirectToAction(nameof(Subproject), new { pr = s.ToString(), GithubLink = subproject.githubLink });
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSubproject(int subprojectId)
        {
            Subproject subproject = await subprojectService.FindProject(subprojectId);
            SubprojectViewModel model = new SubprojectViewModel
            {
                Id = subproject.Id,
                Name = subproject.name,
                Language = subproject.language,
                GithubLink = subproject.githubLink
            };
            return View("Delete", model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSubproject(SubprojectViewModel model)
        {
            await subprojectService.DeleteProject(model.Id);
            return RedirectToAction("Project", "Project");
        }

    }
}
